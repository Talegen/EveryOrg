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
    /// Represents the details of a nonprofit organization for browsing purposes.
    /// </summary>
    public class NonprofitBrowseDetails
    {
        /// <summary>
        /// Gets or sets the nonprofit name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nonprofit EIN (Employer Identification Number).
        /// </summary>
        public string Ein { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the logo URL of the nonprofit organization.
        /// </summary>
        public string LogoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the cover image URL of the nonprofit organization.
        /// </summary>
        public string CoverImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the logo Cloudinary ID of the nonprofit organization.
        /// </summary>
        public string LogoCloudinaryId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the org slug.
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nonprofit location.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nonprofit website URL.
        /// </summary>
        public string WebsiteUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Every.org profile URL of the nonprofit organization.
        /// </summary>
        public string ProfileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a list of tags associated with the nonprofit organization.
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

    }
}
