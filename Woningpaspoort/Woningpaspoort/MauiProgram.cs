using Camera.MAUI;
using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using Woningpaspoort.Services;
using Woningpaspoort.View.Mobile;
using Woningpaspoort.ViewModel;
using Woningpaspoort.Resources.Languages;
using CommunityToolkit.Maui.Storage;

namespace Woningpaspoort;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseLocalizationResourceManager(settings =>
            {
                settings.RestoreLatestCulture(true);
                settings.AddResource(AppResources.ResourceManager);
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseSimpleToolkit()
            .UseSimpleShell()
            .UseMauiMaps()
            .ConfigureMopups()
            .UseMauiCameraView();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<HouseService>();
        builder.Services.AddSingleton<ImageService>();
        builder.Services.AddSingleton<DocumentService>();

        builder.Services.AddSingleton<HousesViewModel>();
        builder.Services.AddSingleton<HouseDetailsViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<HouseDetailsPage>();
        builder.Services.AddTransient<MapsPage>();
        builder.Services.AddTransient<WorkPage>();
        builder.Services.AddTransient<DocumentsPage>();
        builder.Services.AddTransient<PhotosPage>();
        builder.Services.AddTransient<FilterPage>();
        builder.Services.AddTransient<QrScanPage>();
        builder.Services.AddTransient<ManagePortalPage>();
        builder.Services.AddTransient<AddHousePage>();

        return builder.Build();
	}
}
