namespace DonateOrBust;

using DonateOrBust.ViewModels;
using Talegen.EveryOrg.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
                fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
                fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<ListDetailDetailPage>();


        builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddEveryOrgClient(config =>
        {
            config.PublicKey = "pk_live_2694f5c9d13dcfdec600551e6e2930a8";
        });
        return builder.Build();
    }
}
