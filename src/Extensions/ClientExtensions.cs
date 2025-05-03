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
namespace Talegen.EveryOrg.Client.Extensions
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class contains extension and helper methods for the client.
    /// </summary>
    internal static class ClientExtensions
    {
        /// <summary>
        /// Contains the allowed causes for the API.
        /// </summary>
        /// <remarks>
        /// See https://docs.every.org/docs/types#causes
        /// </remarks>
        private static readonly string[] AllowedCauses = new[]
        {
           "aapi-led",
           "adoption",
           "afghanistan",
           "animals",
           "art",
           "athletics",
           "autism",
           "black-led",
           "buddhism",
           "cancer",
           "cats",
           "christianity",
           "climate",
           "conservation",
           "coronavirus",
           "culture",
           "dance",
           "disabilities",
           "disease",
           "dogs",
           "education",
           "environment",
           "filmandtv",
           "food-security",
           "freepress",
           "gender-equality",
           "health",
           "hinduism",
           "housing",
           "humans",
           "hurricane-ian",
           "immigrants",
           "indigenous-led",
           "indigenous-peoples",
           "islam",
           "judaism",
           "justice",
           "latine-led",
           "legal",
           "lgbt",
           "libraries",
           "mental-health",
           "museums",
           "music",
           "oceans",
           "parks",
           "poverty",
           "racial-justice",
           "radio",
           "refugees",
           "religion",
           "research",
           "science",
           "seniors",
           "space",
           "theater",
           "transgender",
           "ukraine",
           "veterans",
           "votingrights",
           "water",
           "wildfires",
           "wildlife",
           "women-led",
           "womens-health",
           "youth"
        };

        /// <summary>
        /// Creates the authentication header for the client settings.
        /// </summary>
        /// <param name="clientSettings">Contains the client settings containing keys.</param>
        /// <returns>The authentication header.</returns>
        /// <exception cref="ArgumentNullException">Thrown when client settings are null.</exception>"
        public static string PrivateAuthenticationBuilder(this ClientSettings clientSettings)
        {
            if (clientSettings == null)
            {
                throw new ArgumentNullException(nameof(clientSettings));
            }
         
            var sb = new StringBuilder();

            sb.Append("Basic ");
            sb.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientSettings.PublicKey}:{clientSettings.PrivateKey}")));

            return sb.ToString();
        }

        /// <summary>
        /// Checks if the causes are allowed.
        /// </summary>
        /// <param name="causes">Contains a comma delimited string of causes.</param>
        /// <returns>Returns a value indicating if all causes in passed causes are verified allowed.</returns>
        public static bool IsAllowedCauses(string causes)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(causes))
            {
                string[] causesArray = causes.Split(',', StringSplitOptions.RemoveEmptyEntries);
                result = IsAllowedCauses(causesArray);
            }
            return result;
        }

        /// <summary>
        /// Checks if the causes are allowed.
        /// </summary>
        /// <param name="causes">Contains an array of causes to check.</param>
        /// <returns>Returns a value indicating if all causes in passed causes are verified allowed.</returns>
        public static bool IsAllowedCauses(string[] causes)
        {
            bool result = false;

            if (causes != null && causes.Length > 0)
            {
                result = causes.All(cause => AllowedCauses.Contains(cause, StringComparer.OrdinalIgnoreCase));
            }
            
            return result;
        }
    }
}
