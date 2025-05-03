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
    /// This class contains the NTEE code meaning.
    /// </summary>
    public class NteeCodeMeaning
    {
        /// <summary>
        /// Gets or sets the NTEE code.
        /// </summary>
        public string MajorCode { get; set; }

        /// <summary>
        /// Gets or sets the NTEE code meaning.
        /// </summary>
        public string MajorMeaning { get; set; }

        /// <summary>
        /// Gets or sets the NTEE Decil code.
        /// </summary>
        public string DecileCode { get; set; }

        /// <summary>
        /// Gets or sets the NTEE Decil code meaning.
        /// </summary>
        public string DecileMeaning { get; set; }
    }
}
