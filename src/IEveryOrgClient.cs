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
namespace Talegen.EveryOrg.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Talegen.EveryOrg.Client.Models;

    /// <summary>
    /// Interface for EveryOrg client.
    /// </summary>
    public interface IEveryOrgClient
    {
        /// <summary>
        /// Gets a list of nonprofit organizations by cause.
        /// </summary>
        /// <param name="cause">Contains the cause to search.</param>
        /// <param name="page">Contains the current page number.</param>
        /// <param name="pageSize">Contains the page size.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the browse results.</returns>
        Task<BrowseResult> BrowseByCauseAsync(string cause, int page = 1, int pageSize = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the details of a nonprofit organization by its ID.
        /// </summary>
        /// <param name="orgId">Contains the organization id to retrieve details.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns a non-profile details envelope <see cref="DetailsResult"/> if found</returns>
        Task<DetailsResult> GetDetailsAsync(string orgId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Searches for nonprofit organizations based on a keyword and causes.
        /// </summary>
        /// <param name="keyword">Contains the keyword to search for.</param>
        /// <param name="causes">Contains an optional comma-delimited list of causes to filter by.</param>
        /// <param name="resultSize">Contains a maximum result size for results. 50 is maximum.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the search results.</returns>
        Task<SearchResult> SearchAsync(string? keyword = null, string? causes = null, int resultSize = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a fundraiser for a nonprofit organization.
        /// </summary>
        /// <param name="orgId">Contains the nonprofit organization identifier.</param>
        /// <param name="fundraiserId">Contains the unique identifier of the fundraiser.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the fundraiser result.</returns>
        Task<Fundraiser> GetFundraiserAsync(string orgId, string fundraiserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the fundraiser financial information for a nonprofit organization.
        /// </summary>
        /// <param name="orgId">Contains the nonprofit organization identifier.</param>
        /// <param name="fundraiserId">Contains the unique identifier of the fundraiser.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns>Returns the fundraiser financial details.</returns>
        Task<FundraiserRaised> GetFundraiserRaisedAsync(string orgId, string fundraiserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method is used to create a new fundraiser for a nonprofit organization.
        /// </summary>
        /// <param name="request">Contains the fundraiser request.</param>
        /// <param name="cancellationToken">Contains an optional cancellation token.</param>
        /// <returns></returns>
        Task<Fundraiser> CreateFundraiserAsync(FundraiserRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method creates a donation link for a nonprofit organization.
        /// </summary>
        /// <param name="request">Contains the donation link creation request.</param>
        /// <returns>Returns the donation result containing the donation link.</returns>
        DonateResult CreateDonation(DonateRequest request);
    }
}
