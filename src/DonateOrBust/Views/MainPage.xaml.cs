namespace DonateOrBust.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DonateOrBust.ViewModels;
using Microsoft.Maui.Controls;
using Talegen.EveryOrg.Client;
using Talegen.EveryOrg.Client.Models;

public partial class MainPage : ContentPage
{
    private readonly List<NonprofitBrowseDetails> organizations;

    private readonly IEveryOrgClient everyOrgClient;

    private readonly MainPageViewModel viewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        this.BindingContext = this.viewModel = viewModel;
        this.InitializeComponent();

    }
}