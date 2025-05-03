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
    /// <summary>
    /// This class contains the API constants for the client.
    /// </summary>
    internal static class ApiConstants
    {
        /// <summary>
        /// Contains the base URL for the API.
        /// </summary>
        public const string BaseUrl = "https://partners.every.org/";

        /// <summary>
        /// Contains the base URL for the production API.
        /// </summary>
        public const string BaseDonationProductionUrl = "https://www.every.org/{0}#donate";

        /// <summary>
        /// Contains the base URL for the sandbox API.
        /// </summary>
        public const string BaseDonationSandboxUrl = "https://staging.every.org/{0}#donate";

        /// <summary>
        /// Contains the Public HTTP Client key
        /// </summary>
        public const string PublicClientKey = "PublicApiEveryOrgClient";

        /// <summary>
        /// Contains the Private HTTP Client key
        /// </summary>
        public const string PrivateClientKey = "PrivateApiEveryOrgClient";

        /// <summary>
        /// Contains the Idempotency header name.
        /// </summary>
        public const string HeaderIdempotencyKey = "X-Idempotency-Key";

        /// <summary>
        /// Contains the User agent header name.
        /// </summary>
        public const string HeaderUserAgent = "User-Agent";

        /// <summary>
        /// Contains the client accept header name.
        /// </summary>
        public const string HeaderAccept = "Accept";

        /// <summary>
        /// Contains the default client accept header value.
        /// </summary>
        public const string HeaderAcceptDefault = "application/json";

        /// <summary>
        /// Contains the content type header name.
        /// </summary>
        public const string HeaderContentType = "Content-Type";

        /// <summary>
        /// Contains the default content type header value.
        /// </summary>
        public const string HeaderContentTypeDefault = "application/json";

        /// <summary>
        /// Contains the authorization header name.
        /// </summary>
        public const string HeaderAuthorization = "Authorization";
    }
}
