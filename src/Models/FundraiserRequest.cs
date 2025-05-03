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
    /// Represents a request to create or update a fundraiser.
    /// </summary>
    public class FundraiserRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the fundraiser organization.
        /// </summary>
        public Guid NonprofitId { get; set; }

        /// <summary>
        /// Gets or sets the title of the fundraiser.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a description of the fundraiser.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the fundraiser start date and time.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the fundraiser end date and time.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the goal amount for the fundraiser.
        /// </summary>
        public decimal Goal { get; set; }

        /// <summary>
        /// Gets or sets an optional amount raised offline.
        /// </summary>
        public decimal? RaisedOffline { get; set; }

        /// <summary>
        /// Gets or sets the currency code for the fundraiser.
        /// </summary>
        public string Currency { get; set; } = "USD";

        /// <summary>
        /// Gets or sets an optional base64 encoded image for the fundraiser.
        /// </summary>
        public string ImageBase64 { get; set; } = string.Empty;
    }
}
