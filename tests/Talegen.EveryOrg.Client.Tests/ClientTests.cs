namespace Talegen.EveryOrg.Client.Tests
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This class contains the tests for the EveryOrgClient class.
    /// </summary>
    public class ClientTests : IClassFixture<TestFixture>
    {
        /// <summary>
        /// The test fixture that provides the client instance.
        /// </summary>
        private readonly TestFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTests"/> class.
        /// </summary>
        public ClientTests(TestFixture testFixture)
        {
            this.fixture = testFixture;
        }

        /// <summary>
        /// Tests the GetNonprofit method of the EveryOrgClient class.
        /// </summary>
        /// <returns>Returns a task.</returns>
        [Fact]
        public async Task TestGetNonprofitExists()
        {
            // Arrange
            string orgId = "heart-of-the-bride";
            var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");

            // Act
            var result = await everyOrgClient.GetDetailsAsync(orgId, TestContext.Current.CancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orgId, result.Data.Nonprofit.PrimarySlug);
        }

        /// <summary>
        /// Tests the GetNonprofit method of the EveryOrgClient class when the nonprofit does not exist.
        /// </summary>
        /// <returns>Contains a task</returns>
        [Fact]
        public async Task TestGetNonprofitNotExists()
        {
            await Assert.ThrowsAnyAsync<ApiClientException>(async () =>
            {
                var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");
                await everyOrgClient.GetDetailsAsync("nonexistent-org", TestContext.Current.CancellationToken);
            });
        }

        /// <summary>
        /// Tests the BrowseByCauseAsync method of the EveryOrgClient class.
        /// </summary>
        /// <param name="cause">Test cause.</param>
        /// <param name="page">Test current page.</param>
        /// <param name="pageSize">Test page size.</param>
        /// <param name="successExpected">Is test success expected.</param>
        /// <param name="expectedExceptionType">If no success, what is the expected exception type.</param>
        /// <returns>Returns task.</returns>
        /// <exception cref="InvalidOperationException">Thrown if client not created.</exception>
        [Theory]
        [InlineData("dogs", 1, 50, true)]
        [InlineData("cats", 2, 50, true)]
        [InlineData("not-valid", 1, 50, false, typeof(ArgumentOutOfRangeException))]
        [InlineData("dogs", 0, 51, false, typeof(ArgumentOutOfRangeException))]
        [InlineData("dogs", 1, 0, false, typeof(ArgumentOutOfRangeException))]
        [InlineData("dogs", 1, 1000, false, typeof(ArgumentOutOfRangeException))]
        public async Task TestBrowseByCauses(string cause, int page, int pageSize, bool successExpected, Type expectedExceptionType = default)
        {
            var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");

            if (successExpected)
            {
                // Act
                var result = await everyOrgClient.BrowseByCauseAsync(cause, page, pageSize, TestContext.Current.CancellationToken);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(page, result.Pagination.Page);
                Assert.Equal(pageSize, result.Pagination.PageSize);
            }
            else
            {
                // validate exceptions
                await Assert.ThrowsAsync(expectedExceptionType, async () =>
                {
                    await everyOrgClient.BrowseByCauseAsync(cause, page, pageSize, TestContext.Current.CancellationToken);
                });
            }
        }

        /// <summary>
        /// Tests the SearchAsync method of the EveryOrgClient class.
        /// </summary>
        /// <param name="search">Search text.</param>
        /// <param name="causes">Optional causes filter.</param>
        /// <param name="resultSize">Test page size.</param>
        /// <param name="successExpected">Is test success expected.</param>
        /// <param name="expectedExceptionType">If no success, what is the expected exception type.</param>
        /// <returns>Returns task.</returns>
        /// <exception cref="InvalidOperationException">Thrown if client not created.</exception>
        [Theory]
        [InlineData("dogs", "animals", 50, 50, true)]
        [InlineData("cats", "animals", 50, 50, true)]
        [InlineData("not-valid", "animal-welfare", 51, 50, false, typeof(ArgumentOutOfRangeException))]
        [InlineData("dogs", "animals", 0, 50, false, typeof(ArgumentOutOfRangeException))]
        public async Task TestSearch(string search, string causes, int resultSize, int expectedResultCount, bool successExpected, Type expectedExceptionType = default)
        {
            var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");
            if (successExpected)
            {
                // Act
                var result = await everyOrgClient.SearchAsync(search, causes, resultSize, TestContext.Current.CancellationToken);
                // Assert
                Assert.NotNull(result);
                Assert.Contains(result.Nonprofits, nonprofit => nonprofit.Name.Contains(search));
                Assert.Equal(expectedResultCount, result.Nonprofits.Count);
            }
            else
            {
                // validate exceptions
                await Assert.ThrowsAsync(expectedExceptionType, async () =>
                {
                    await everyOrgClient.SearchAsync(search, causes, resultSize, TestContext.Current.CancellationToken);
                });
            }
        }

        /// <summary>
        /// Tests the GetFundraiser method of the EveryOrgClient class.
        /// </summary>
        /// <param name="orgId">Contains the org id.</param>
        /// <param name="fundraiserId">Contains the fundraiser id</param>
        /// <param name="successExpected">Is test success expected.</param>
        /// <param name="expectedExceptionType">If no success, what is the expected exception type.</param>
        /// <returns>Returns task.</returns>
        /// <exception cref="InvalidOperationException">Thrown if client not created.</exception>
        [Theory]
        [InlineData("sos", "fundraiser-seattle-sos", false, typeof(ApiClientException))]
        public async Task TestGetFundraiser(string orgId, string fundraiserId, bool successExpected, Type expectedExceptionType = default)
        {
            var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");
            if (successExpected)
            {
                // Act
                var result = await everyOrgClient.GetFundraiserAsync(orgId, fundraiserId, TestContext.Current.CancellationToken);
                // Assert
                Assert.NotNull(result);
                Assert.Equal(fundraiserId, result.Title);
            }
            else
            {
                // validate exceptions
                await Assert.ThrowsAsync(expectedExceptionType, async () =>
                {
                    await everyOrgClient.GetFundraiserAsync(orgId, fundraiserId, TestContext.Current.CancellationToken);
                });
            }
        }

        /// <summary>
        /// Tests the CreateDonation method of the EveryOrgClient class.
        /// </summary>
        [Fact]
        public void TestDonateLink()
        {
            var everyOrgClient = this.fixture.ServiceProvider.GetService<IEveryOrgClient>() ?? throw new InvalidOperationException("IEveryOrgClient is not registered in the service provider.");
            var orgId = "heart-of-the-bride";
            var fundraiserId = "talegen-fundraiser";
            var expectedLink = $"https://staging.every.org/{orgId}";
            
            // Act
            var result = everyOrgClient.CreateDonation(new Models.DonateRequest
            {
               Environment = Models.DonationEnvironment.Sandbox,
               OrganizationId = orgId,
               Method = Models.PaymentMethod.Crypto,
               FirstName = "John",
               LastName = "Doe",
            });

            // Assert
            Assert.NotNull(result);
            Assert.StartsWith(expectedLink, result.DonationUrl);
        }
    }
}