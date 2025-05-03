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
    using System.Collections.Generic;

    /// <summary>
    /// Represents the details of a nonprofit organization returned from a search.
    /// </summary>
    public class NonprofitSearchDetails
    {
        /// <summary>
        /// Gets or sets the name of the nonprofit organization.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nonprofit Every.org profile url.
        /// </summary>
        public string ProfileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set the description of the nonprofit organization.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the EIN (Employer Identification Number) of the nonprofit organization.
        /// </summary>
        public string Ein { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Logo Cloudinary ID of the nonprofit organization.
        /// </summary>
        public string LogoCloudinaryId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the logo URL of the nonprofit organization.
        /// </summary>
        public string LogoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the website URL of the nonprofit organization.
        /// </summary>
        public string WebsiteUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a list of matched terms that were found in the search.
        /// </summary>
        public List<string> MatchedTerms { get; set; } = new List<string>();
    }
}
