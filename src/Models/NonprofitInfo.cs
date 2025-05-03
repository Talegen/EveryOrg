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
    using System;

    /// <summary>
    /// This class contains the Nonprofit information.
    /// </summary>
    public class NonprofitInfo
    {
        /// <summary>
        /// Gets or sets the Nonprofit Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Slug.
        /// </summary>
        public string PrimarySlug { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Slug.
        /// </summary>
        public string Ein { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the Nonprofit is a 501c3.
        /// </summary>
        public bool IsDisbursable { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit short Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit long Description.
        /// </summary>
        public string DescriptionLong { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Address.
        /// </summary>
        public string LocationAddress { get; set; }

        /// <summary>
        /// Gets or sets the non-profilt NTEE code.
        /// </summary>
        public string NteeCode { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit NTEE code meaning.
        /// </summary>
        public NteeCodeMeaning? NteeCodeMeaning { get; set; }

        /// <summary>
        /// Gets or sets the logo Cloudinary Id.
        /// </summary>
        public string LogoCloudinaryId { get; set; }

        /// <summary>
        /// Gets or sets the cover image Cloudinary Id.
        /// </summary>
        public string CoverImageCloudinaryId { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Logo URL.
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Cover Image URL.
        /// </summary>
        public string CoverImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Every.org Profile URL.
        /// </summary>
        public string ProfileUrl { get; set; }

        /// <summary>
        /// Gets or sets the Nonprofit Website URL.
        /// </summary>
        public string WebsiteUrl { get; set; }
    }
}
