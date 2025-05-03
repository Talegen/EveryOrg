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
namespace Talegen.EveryOrg.Client.Models
{
    /// <summary>
    /// Represents a nonprofit organization in the webhook request.
    /// </summary>
    public class WebhookNonprofit
    {
        /// <summary>
        /// Gets or sets the slug of the nonprofit organization.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the EIN (Employer Identification Number) of the nonprofit organization.
        /// </summary>
        public string Ein { get; set; }

        /// <summary>
        /// Gets or sets the name of the nonprofit organization.
        /// </summary>
        public string Name { get; set; }
    }
}
