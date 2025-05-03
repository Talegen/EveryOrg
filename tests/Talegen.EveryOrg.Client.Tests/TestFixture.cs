namespace Talegen.EveryOrg.Client.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// This class contains the test fixture for the EveryOrgClient tests.
    /// </summary>
    public class TestFixture : IDisposable
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        private Microsoft.Extensions.Configuration.IConfiguration Configuration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        public TestFixture()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    Configuration = new ConfigurationBuilder()
                        .AddUserSecrets<TestFixture>()
                        .AddEnvironmentVariables()
                        .Build();

                    string publicKey = Configuration.GetValue<string>("PublicKey") ?? string.Empty;
                    string privateKey = Configuration.GetValue<string>("PrivateKey") ?? string.Empty;

                    // Register services here
                    services.AddEveryOrgClient(config =>
                    {
                        config.PrivateKey = privateKey;
                        config.PublicKey = publicKey;
                    });
                })
                .Build();

            ServiceProvider = host.Services;
        }

        /// <summary>
        /// Disposes the test fixture.
        /// </summary>
        public void Dispose()
        {
            // Clean up resources if needed
        }
    }
}
