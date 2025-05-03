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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Talegen.EveryOrg.Client.Extensions;
    using Talegen.EveryOrg.Client.Models;

    /// <summary>
    /// This class implements the IEveryOrgClient interface and provides methods to interact with the EveryOrg API.
    /// </summary>
    public class EveryOrgClient : IEveryOrgClient
    {
        /// <summary>
        /// Contains the api version to include in the request.
        /// </summary>
        private const string ApiVersion = "v0.2";

        /// <summary>
        /// The client settings for the API client.
        /// </summary>
        private readonly ClientSettings clientSettings;

        /// <summary>
        /// Contains the HTTP client factory to create HTTP clients.
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EveryOrgClient"/> class.
        /// </summary>
        /// <param name="optionsSnapshot">Contains the options snapshot</param>
        /// <exception cref="ArgumentNullException">Thrown if the options snapshot is null or client settings are not found.</exception>
        public EveryOrgClient(IOptionsSnapshot<ClientSettings> optionsSnapshot, IHttpClientFactory httpClientFactory)
        {
            if (optionsSnapshot == null)
            {
                throw new ArgumentNullException(nameof(optionsSnapshot));
            }

            this.clientSettings = optionsSnapshot.Value;
            
            if (this.clientSettings == null)
            {
                throw new ArgumentNullException(nameof(this.clientSettings));
            }

            if (string.IsNullOrWhiteSpace(this.clientSettings.PublicKey))
            {
                throw new ApiClientException("Client settings must contain at least a public key.");
            }

            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Gets a list of nonprofit organizations by cause.
        /// </summary>
        /// <param name="cause">Contains the cause to search.</param>
        /// <param name="page">Contains the current page number.</param>
        /// <param name="pageSize">Contains the page size.</param>
        /// <returns>Returns the browse results.</returns>
        /// <exception cref="ArgumentNullException">Thrown if cause is not specified.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if paging parameters are out of range. page>0, pageSize <= 50</exception>
        /// <exception cref="ApiClientException">Thrown if an HTTP error occurs.</exception>
        public async Task<BrowseResult> BrowseByCauseAsync(string cause, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default)
        {
            BrowseResult resultModel;

            if (string.IsNullOrWhiteSpace(cause))
            {
                throw new ArgumentNullException(nameof(cause), "Cause must be specified.");
            }

            // Check if the causes are allowed
            if (!ClientExtensions.IsAllowedCauses(cause))
            {
                throw new ArgumentOutOfRangeException(nameof(cause), "The cause specified is not in the allowed list of causes. See https://docs.every.org/docs/types#causes");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            if (pageSize > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be less than or equal to 50.");
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PublicClientKey);
            var url = $"/{ApiVersion}/browse/{cause}?take={pageSize}&page={page}&apiKey={this.clientSettings.PublicKey}";
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                // Make the request and get the response
                using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<BrowseResult>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request {0}. Status: {1}", url, statusCode);

                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The API {0} was not found.", url);
                }

                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }

            return resultModel;
        }

        /// <summary>
        /// Gets the details of a nonprofit organization by its ID.
        /// </summary>
        /// <param name="orgId">Contains the organization id to retrieve details.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the organization is not specified.</exception>
        /// <exception cref="ApiClientException">Thrown if a 404 or other bad HTTP response occurs.</exception>
        public async Task<DetailsResult> GetDetailsAsync(string orgId, CancellationToken cancellationToken = default)
        {
            DetailsResult resultModel = null;

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new ArgumentNullException(nameof(orgId));
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PublicClientKey);
            var url = $"/{ApiVersion}/nonprofit/{Uri.EscapeDataString(orgId)}?apiKey={this.clientSettings.PublicKey}";
            HttpStatusCode statusCode = HttpStatusCode.OK;
            
            try
            {
                // Make the request and get the response
                using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<DetailsResult>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request {0}. Status: {1}", url, statusCode);

                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The nonprofit with the id {0} was not found.", orgId);
                }

                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }

            return resultModel;
        }

        /// <summary>
        /// Searches for nonprofit organizations based on a keyword and causes.
        /// </summary>
        /// <param name="keyword">Contains the keyword to search for.</param>
        /// <param name="causes">Contains an optional comma-delimited list of causes to filter by.</param>
        /// <param name="resultSize">Contains a maximum result size for results. 50 is maximum.</param>
        /// <returns>Returns the search results.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the organization is not specified.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if result size is out of range. 50 maximum results.</exception>
        /// <exception cref="ApiClientException">Thrown if a 404 or other bad HTTP response occurs.</exception>
        public async Task<SearchResult> SearchAsync(string? keyword = null, string? causes = null, int resultSize = 50, CancellationToken cancellationToken = default)
        {
            SearchResult resultModel;

            if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(causes))
            {
                throw new ArgumentNullException(nameof(keyword), "Either keyword or causes must be specified.");
            }

            if (resultSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(resultSize), "Result size must be greater than or equal to 1.");
            }

            if (resultSize > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(resultSize), "Result size must be less than or equal to 50.");
            }

            // Check if the causes are allowed
            if (!string.IsNullOrWhiteSpace(causes) && !ClientExtensions.IsAllowedCauses(causes))
            {
                throw new ArgumentOutOfRangeException(nameof(causes), "One or more causes specified are not in the allowed list of causes. See https://docs.every.org/docs/types#causes");
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PublicClientKey);
            var url = $"/{ApiVersion}/search/{Uri.EscapeDataString(keyword)}?take={resultSize}&";
            
            if (!string.IsNullOrWhiteSpace(causes))
            {
                url += $"causes={causes}&";
            }

            url += $"apiKey={this.clientSettings.PublicKey}";
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                // Make the request and get the response
                using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<SearchResult>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request {0}. Status: {1}", url, statusCode);

                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The API {0} was not found.", url);
                }

                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }

            return resultModel;
        }

        /// <summary>
        /// Gets a fundraiser for a nonprofit organization.
        /// </summary>
        /// <param name="orgId">Contains the nonprofit organization identifier.</param>
        /// <param name="fundraiserId">Contains the unique identifier of the fundraiser.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the fundraiser result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the org or fundraiser identifiers are not specified.</exception>
        /// <exception cref="ApiClientException">Thrown if a 404 or other bad HTTP response occurs.</exception>
        public async Task<Fundraiser> GetFundraiserAsync(string orgId, string fundraiserId, CancellationToken cancellationToken = default)
        {
            Fundraiser resultModel = null;

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new ArgumentNullException(nameof(orgId), "Organization ID must be specified.");
            }

            if (string.IsNullOrWhiteSpace(fundraiserId))
            {
                throw new ArgumentNullException(nameof(fundraiserId), "Fundraiser ID must be specified.");
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PublicClientKey);
            var url = $"/{ApiVersion}/fundraiser/{Uri.EscapeDataString(orgId)}/{Uri.EscapeDataString(fundraiserId)}";
            HttpStatusCode statusCode = HttpStatusCode.OK;
            
            try
            {
                // Make the request and get the response
                using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<Fundraiser>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request {0}. Status: {1}", url, statusCode);
                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The API {0} was not found.", url);
                }
                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }
            return resultModel;
        }

        /// <summary>
        /// This method is used to get the fundraiser financial information for a nonprofit organization.
        /// </summary>
        /// <param name="orgId">Contains the nonprofit organization identifier.</param>
        /// <param name="fundraiserId">Contains the unique identifier of the fundraiser.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the fundraiser financial details.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the org or fundraiser identifiers are not specified.</exception>
        /// <exception cref="ApiClientException">Thrown if a 404 or other bad HTTP response occurs.</exception>
        public async Task<FundraiserRaised> GetFundraiserRaisedAsync(string orgId, string fundraiserId, CancellationToken cancellationToken = default)
        {
            FundraiserRaised resultModel = null;

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new ArgumentNullException(nameof(orgId), "Organization ID must be specified.");
            }

            if (string.IsNullOrWhiteSpace(fundraiserId))
            {
                throw new ArgumentNullException(nameof(fundraiserId), "Fundraiser ID must be specified.");
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PublicClientKey);
            var url = $"/{ApiVersion}/fundraiser/{Uri.EscapeDataString(orgId)}/{Uri.EscapeDataString(fundraiserId)}/raised";
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                // Make the request and get the response
                using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<FundraiserRaised>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request {0}. Status: {1}", url, statusCode);
                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The API {0} was not found.", url);
                }
                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }
            return resultModel;
        }

        /// <summary>
        /// This method is used to create a new fundraiser for a nonprofit organization.
        /// </summary>
        /// <param name="request">Contains the fundraiser request.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the newly created fundraiser object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the request is not defined.</exception>
        /// <exception cref="ApiClientException">Thrown if a bad HTTP response occurs.</exception>
        public async Task<Fundraiser> CreateFundraiserAsync(FundraiserRequest request, CancellationToken cancellationToken = default)
        {
            Fundraiser resultModel;

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Either keyword or causes must be specified.");
            }

            // Create the HTTP client and make the request
            using var client = this.httpClientFactory.CreateClient(ApiConstants.PrivateClientKey);
            var url = $"/{ApiVersion}/fundraiser";
            HttpStatusCode statusCode = HttpStatusCode.OK;

            try
            {
                // Make the request and get the response
                using var bodyContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, ApiConstants.HeaderContentTypeDefault);
                using HttpResponseMessage response = await client.PostAsync(url, bodyContent, cancellationToken);
                statusCode = response.StatusCode;
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                resultModel = JsonConvert.DeserializeObject<Fundraiser>(responseContent);
            }
            catch (HttpRequestException hex)
            {
                string httpMessage = string.Format("Error HTTP occurred while making the request POST {0}. Status: {1}", url, statusCode);

                if (statusCode == HttpStatusCode.NotFound)
                {
                    httpMessage = string.Format("The API {0} was not found.", url);
                }

                throw new ApiClientException(httpMessage, hex, (int)statusCode);
            }
            catch (Exception ex)
            {
                throw new ApiClientException(string.Format("A general error occurred while making the request {0}. Status: {1}", url, statusCode), ex, (int)statusCode);
            }

            return resultModel;
        }

        /// <summary>
        /// This method creates a donation link for a nonprofit organization.
        /// </summary>
        /// <param name="request">Contains the donation link creation request.</param>
        /// <returns>Returns the donation result containing the donation link.</returns>
        public DonateResult CreateDonation(DonateRequest request)
        {
            string urlTemplate = request.Environment == DonationEnvironment.Production ? ApiConstants.BaseDonationProductionUrl : ApiConstants.BaseDonationSandboxUrl;

            // fast hack to build query string from object JSON serialization/deserialization
            var step1 = JsonConvert.SerializeObject(request,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            var step2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(step1);
            var step3 = step2.Select(x =>
            {
                string paramValue = HttpUtility.UrlEncode(x.Key) + "=";
                Type valueType = x.Value.GetType();

                if (x.Value is Newtonsoft.Json.Linq.JArray || valueType.IsSZArray || valueType.IsVariableBoundArray)
                {
                    paramValue += string.Join(',', x.Value as IEnumerable);
                }
                else 
                {
                    paramValue += HttpUtility.UrlEncode(x.Value.ToString());
                }

                return paramValue;
            });
            return new DonateResult
            {
                DonationUrl = string.Format(urlTemplate, request.OrganizationId + "?" + string.Join("&", step3))
            };
        }
    }
}
