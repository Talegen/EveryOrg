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
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a webhook request for a donation made through EveryOrg.
    /// </summary>
    public class WebhookRequest
    {
        /// <summary>
        /// Gets or sets the identifier of the webhook request.
        /// </summary>
        public Guid ChargeId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the partner donation.
        /// </summary>
        public Guid PartnerDonationId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the donor.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the donor.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the donor.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the nonprofit organization that received the donation.
        /// </summary>
        public WebhookNonprofit ToNonprofit { get; set; } = new WebhookNonprofit();

        /// <summary>
        /// Gets or sets the amount of the donation.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the net amount of the donation after fees.
        /// </summary>
        public decimal NetAmount { get; set; }

        /// <summary>
        /// Gets the fees associated with the donation.
        /// </summary>
        [JsonIgnore]
        public decimal Fees => this.Amount - this.NetAmount;

        /// <summary>
        /// Gets or sets the currency of the donation amount.
        /// </summary>
        public string Currency { get; set; } = "USD";

        /// <summary>
        /// Gets or sets the frequency of the donation (e.g., Monthly, Yearly).
        /// </summary>
        public string Frequency { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the donation was made.
        /// </summary>
        public DateTime DonationDate { get; set; }

        /// <summary>
        /// Gets or sets any public testimony provided by the donor.
        /// </summary>
        public string PublicTestimony { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets any private note provided by the donor.
        /// </summary>
        public string PrivateNote { get; set; } = string.Empty;
    }
}
