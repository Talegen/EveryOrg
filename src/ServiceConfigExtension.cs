/*
 * Talegen Every.org API Client Library
 * (c) Copyright Talegen, LLC.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/
namespace Talegen.EveryOrg.Client
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Polly;
    using Polly.Contrib.WaitAndRetry;
    using Polly.Extensions.Http;
    using Polly.Timeout;
    using Talegen.EveryOrg.Client.Extensions;

    /// <summary>
    /// This class contains extension methods for configuring the service collection for the EveryOrg API client.
    /// </summary>
    public static class ServiceConfigExtension
    {
        /// <summary>
        /// Adds the EveryOrg client to the service collection with the specified configuration.
        /// </summary>
        /// <param name="services">Contains the service collection to add services to.</param>
        /// <param name="config">Contains the configuration action to configure the client settings.</param>
        /// <returns>Returns the updated service collection.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any argument is not defined.</exception>
        public static IServiceCollection AddEveryOrgClient(this IServiceCollection services, Action<ClientSettings> config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();
            services.Configure(config);

            // get the settings from config for retry policy
            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<ClientSettings>>()?.Value ?? new ClientSettings();

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            // add the client with the settings
            return services.BuildClients(settings);
        }

        /// <summary>
        /// Builds the clients for the service collection.
        /// </summary>
        /// <param name="services">Contains the service collection to add services to.</param>
        /// <param name="settings">Contains the client settings.</param>
        /// <returns>Returns the updated service collection.</returns>
        private static IServiceCollection BuildClients(this IServiceCollection services, ClientSettings settings)
        {
            // build the retry policy
            var jitterRetry = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
                .WaitAndRetryAsync(jitterRetry,
                (response, timespan, context) =>
                {
                    if (settings.IncludeIdempotency &&
                        context["httpClient"] is HttpClient client &&
                        client.DefaultRequestHeaders.Contains(ApiConstants.HeaderIdempotencyKey))
                    {
                        // remove the old idempotency key
                        client.DefaultRequestHeaders.Remove(ApiConstants.HeaderIdempotencyKey);
                        client.DefaultRequestHeaders.Add(ApiConstants.HeaderIdempotencyKey, Guid.NewGuid().ToString());
                    }
                });

            // create a new http client for private API calls
            // Public API clients (e.g. nonprofit search) will include a public key in their query string.
            services.AddHttpClient(ApiConstants.PublicClientKey, (serviceProvider, client) =>
            {

                client.BaseAddress = new Uri(ApiConstants.BaseUrl);

                // add default headers
                if (!string.IsNullOrWhiteSpace(settings.AgentName))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderUserAgent, settings.AgentName);
                }

                client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderAccept, ApiConstants.HeaderAcceptDefault);
                client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderContentType, ApiConstants.HeaderContentTypeDefault);

                // if idempotency is enabled...
                if (settings.IncludeIdempotency)
                {
                    // add idempotency key header
                    client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderIdempotencyKey, Guid.NewGuid().ToString());
                }
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(retryPolicy);


            // create a new http client for private API calls
            services.AddHttpClient(ApiConstants.PrivateClientKey, (serviceProvider, client) =>
            {

                client.BaseAddress = new Uri(ApiConstants.BaseUrl);

                // add default headers
                if (!string.IsNullOrWhiteSpace(settings.AgentName))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderUserAgent, settings.AgentName);
                }

                client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderAccept, ApiConstants.HeaderAcceptDefault);
                client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderContentType, ApiConstants.HeaderContentTypeDefault);

                // set authorization
                client.DefaultRequestHeaders.TryAddWithoutValidation(ApiConstants.HeaderAuthorization, settings.PrivateAuthenticationBuilder());

                // if idempotency is enabled...
                if (settings.IncludeIdempotency)
                {
                    // add idempotency key header
                    client.DefaultRequestHeaders.Add(ApiConstants.HeaderIdempotencyKey, Guid.NewGuid().ToString());
                }
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(retryPolicy);

            // add scoped service client for every.org
            services.AddScoped<IEveryOrgClient, EveryOrgClient>();
            return services;
        }
    }
}
