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
    /// This class represents a nonprofit tag.
    /// </summary>
    public class NonprofitTag
    {
        /// <summary>
        /// Gets or sets the unique identifier for the nonprofit tag.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public string TagName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the related cause category.
        /// </summary>
        public string CauseCategory { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tag image cloudinary identifier.
        /// </summary>
        public string TagImageCloudinaryId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tag URL.
        /// </summary>
        public string TagUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tag image URL.
        /// </summary>
        public string TagImageUrl { get; set; } = string.Empty;
    }
}
