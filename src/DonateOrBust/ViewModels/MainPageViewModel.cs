using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using Talegen.EveryOrg.Client;
using Talegen.EveryOrg.Client.Models;

namespace DonateOrBust.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IEveryOrgClient everyOrgClient;
        public List<NonprofitBrowseDetails> Organizations { get; set; } = new List<NonprofitBrowseDetails>();

        /// <summary>
        /// The selected organization for donation.
        /// </summary>
        public NonprofitBrowseDetails SelectedOrg { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the selected organization.
        /// </summary>
        public bool ShowSelectedOrg { get; set; }

        /// <summary>
        /// Contains the property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainPageViewModel(IEveryOrgClient everyOrgClient)
        {
            this.everyOrgClient = everyOrgClient;
        }

        [RelayCommand]
        private async Task OnCatsOrDogsTap(CancellationToken cancellationToken)
        {
            await InitializeOrgs(cancellationToken);
            this.ShowSelectedOrg = true;
            // refresh bindings
            this.OnPropertyChanged(nameof(ShowSelectedOrg));
            this.OnPropertyChanged(nameof(SelectedOrg));
        }

        [RelayCommand]
        private async Task OnDonateTap(CancellationToken cancellationToken)
        {
            if (this.Organizations == null || !this.Organizations.Any())
            {
                await DisplayAlert("Error", "No organizations available to donate.", "OK");
                return;
            }

            // open browser to the URL
            string futureWebhookToken = string.Empty;
            var buildUrlResult = this.everyOrgClient.CreateDonation(new DonateRequest
            {
                Amount = 10,
                Email = FakedData.UserEmailAddress,
                FirstName = FakedData.UserFirstName,
                LastName = FakedData.UserLastName,
                OrganizationId = this.SelectedOrg.Slug,
                WebhookToken = futureWebhookToken,

            });

            // Open the URL in the default browser
            await Browser.OpenAsync(buildUrlResult.DonationUrl, BrowserLaunchMode.SystemPreferred);
        }

        [RelayCommand]
        private void OnReferFriendTap(CancellationToken cancellationToken)
        {
            DisplayAlert("Refer a Friend", "Thank you for referring a friend!", "OK");
        }

        private async Task DisplayAlert(string title, string message, string cancel)
        {
            // This method should be implemented to show an alert in the UI.
            // For example, using Xamarin.Forms or MAUI's DisplayAlert method.
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        private async Task InitializeOrgs(CancellationToken cancellationToken)
        {
            var resultDogs = await this.everyOrgClient.BrowseByCauseAsync("dogs", 1, 50, cancellationToken);
            var resultCats = await this.everyOrgClient.BrowseByCauseAsync("cats", 1, 50, cancellationToken);

            this.Organizations.AddRange(resultDogs.Nonprofits);
            this.Organizations.AddRange(resultCats.Nonprofits);

            if (this.Organizations == null || !this.Organizations.Any())
            {
                throw new ArgumentException("No organizations found.");
                // await DisplayAlert("Error", "No organizations found.", "OK");
                return;
            }

            var random = new Random();
            SelectedOrg = this.Organizations[random.Next(this.Organizations.Count)];
            //SelectedOrgLabel.Text = selectedOrg.Name;
        }


        /// <summary>
        /// This method is used to invoke the property changed event.
        /// </summary>
        /// <param name="propertyName">Contains the optional property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
