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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents the type of goal for a fundraiser.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FundraiserGoalType
    {
        CUSTOM,
        AUTOMATIC
    }

    /// <summary>
    /// Represents the financial information for a fundraiser.
    /// </summary>
    public class FundraiserRaised
    {
        /// <summary>
        /// Gets or sets the currency for the fundraiser.
        /// </summary>
        public string Currency { get; set; } = "USD";

        /// <summary>
        /// Gets or sets the amount raised for the fundraiser.
        /// </summary>
        public decimal Raised { get; set; }

        /// <summary>
        /// Gets or sets the number of supporters for the fundraiser.
        /// </summary>
        public int Supporters { get; set; }

        /// <summary>
        /// Gets or sets the goal amount for the fundraiser.
        /// </summary>
        public decimal GoalAmount { get; set; }

        /// <summary>
        /// Gets or sets the type of goal for the fundraiser.
        /// </summary>
        public FundraiserGoalType GoalType { get; set; } = FundraiserGoalType.AUTOMATIC;
    }
}
