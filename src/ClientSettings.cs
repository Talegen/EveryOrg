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
    /// This class contains the client settings for the API client.
    /// </summary>
    public class ClientSettings
    {
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string AgentName { get; set; } = "EveryOrgApiClient";

        /// <summary>
        /// Gets or sets the client API public key.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the client API private key.
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the idempotency key in the request headers.
        /// </summary>
        public bool IncludeIdempotency { get; set; } = false;
    }
}
