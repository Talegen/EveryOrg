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
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents the environment for the donation link.
    /// </summary>
    public enum DonationEnvironment
    {
        /// <summary>
        /// The production environment.
        /// </summary>
        Production,

        /// <summary>
        /// The sandbox environment.
        /// </summary>
        Sandbox
    }

    /// <summary>
    /// Represents the payment methods available for donations.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentMethod
    {
        [EnumMember(Value = "card")]
        Card,
        [EnumMember(Value = "bank")]
        Bank,

        [EnumMember(Value = "paypal")]
        PayPal,

        [EnumMember(Value = "venmo")]
        Venmo,

        [EnumMember(Value = "pay")]
        MobilePay, //(mobile payments)

        [EnumMember(Value = "crypto")]
        Crypto,

        [EnumMember(Value = "stocks")]
        Stocks,

        [EnumMember(Value = "daf")]
        Daf,

        [EnumMember(Value = "gift")]
        GiftCard //(gift cards)
    }

    /// <summary>
    /// Represents the frequency of the donation.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Frequency
    {
        ONCE,
        MONTHLY
    }

    /// <summary>
    /// Represents a request to create a donation link.
    /// </summary>
    public class DonateRequest
    {
        /// <summary>
        /// Gets or sets the environment for the donation link.
        /// </summary>
        [JsonIgnore]
        public DonationEnvironment Environment { get; set; } = DonationEnvironment.Production;

        /// <summary>
        /// Gets or sets the ID of the nonprofit organization to which the donation will be made.
        /// </summary>
        [JsonIgnore]
        [Required]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the amount of the donation.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets a list of up to five suggested donation amounts that will appear as buttons below the amount input field.
        /// </summary>
        [JsonProperty("suggestedAmounts")]
        public List<decimal>? SuggestedAmounts { get; set; }

        /// <summary>
        /// Gets or sets a minimum value for the donation.
        /// </summary>
        [JsonProperty("min_value")]
        public decimal? MinimumAmount { get; set; }

        /// <summary>
        /// Gets or sets the frequency of the donation (ONCE or MONTHLY).
        /// </summary>
        [JsonProperty("frequency")]
        public Frequency? Frequency { get; set; } 

        /// <summary>
        /// Gets or sets the donor's email address.
        /// </summary>
        [EmailAddress]
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the donor's first name.
        /// </summary>
        [JsonProperty("first_name")]
        public string? FirstName { get; set; } 

        /// <summary>
        /// Gets or sets the donor's last name.
        /// </summary>
        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets a description for the donation.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the background of the donation modal.
        /// </summary>
        [JsonProperty("no_exit")]
        public bool? NoExit { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect the user after they have successfully completed their donation.
        /// </summary>
        [JsonProperty("success_url")]
        public string? SuccessUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect the user after clicking the exit button without completing the donation.
        /// </summary>
        [JsonProperty("exit_url")]
        public string? ExitUrl { get; set; }

        /// <summary>
        /// Gets or sets a unique ID that you want to be associated with this donation.
        /// </summary>
        [JsonProperty("partner_donation_id")]
        public string? PartnerDonationId { get; set; }

        /// <summary>
        /// Gets or sets a base64 encoded JSON object that will be forwarded to you in the partner webhook notification.
        /// </summary>
        [JsonProperty("partner_metadata")]
        public string? PartnerMetadata { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to require the donor to share their contact information for the donation.
        /// </summary>
        [JsonProperty("require_share_info")]
        public bool? RequireShareInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to set the sharing info checkbox as checked by default.
        /// </summary>
        [JsonProperty("share_info")]
        public bool? ShareInfo { get; set; }

        /// <summary>
        /// Gets or sets the designation for the donation.
        /// </summary>
        /// <remarks>
        /// Note that Every.org grants are unrestricted, so this is only a recommendation to the nonprofit, not a legal restriction.
        /// </remarks>
        [JsonProperty("designation")]
        public string? Designation { get; set; }

        /// <summary>
        /// Gets or sets the token associated with your Partner Webhook.
        /// </summary>
        /// <remarks>
        /// Including this parameter will cause a notification to be sent to your webhook for every donation completed via this donate link.
        /// </remarks>
        [JsonProperty("webhook_token")]
        public string? WebhookToken { get; set; }

        /// <summary>
        /// Gets or sets a custom primary theme color of the donation modal.
        /// </summary>
        [JsonProperty("theme_color")]
        public string? ThemeColor { get; set; }

        /// <summary>
        /// Gets or sets the allowed donation methods.
        /// </summary>
        [JsonProperty("method")]
        public PaymentMethod? Method { get; set; } 
    }
}
