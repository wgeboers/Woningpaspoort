using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using Mopups.Services;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort.View.Mobile;

public partial class HouseDetailsPage : ContentPage
{
    HouseDetailsViewModel houseDetailsViewModel;
    ILocalizationResourceManager _localizationResourceManager;

    public HouseDetailsPage(HouseDetailsViewModel houseDetailsViewModel, ILocalizationResourceManager localizationResourceManager)
	{
		InitializeComponent();

		BindingContext = houseDetailsViewModel;
        this.houseDetailsViewModel = houseDetailsViewModel;
        _localizationResourceManager = localizationResourceManager;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            DetailsImage.MaximumHeightRequest = 400;
            DetailsImage.MaximumWidthRequest = 600;
        }
        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        {
            DetailsImage.MaximumHeightRequest = 200;
            DetailsImage.MaximumWidthRequest = 300;
        }
    }

    private async void TapGestureRecognizer_NavigateMainPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private async void TapGestureRecognizer_OpenNavigation(object sender, TappedEventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        if (houseDetailsViewModel.House.Latitude != null)
        {
            var location = new Location((double)houseDetailsViewModel.House.Latitude, (double)houseDetailsViewModel.House.Longitude);
            var options = new MapLaunchOptions
            {
                Name = houseDetailsViewModel.House.Code,
                NavigationMode = NavigationMode.Driving
            };

            try
            {
                await Map.Default.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(_localizationResourceManager["NoMap"], ToastDuration.Short, 14);
                await toast.Show(cancellationTokenSource.Token);
            }
        }
        else
        {
            var toast = Toast.Make(_localizationResourceManager["NoLocation"], ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void TapGestureRecognizer_QRpopUp(object sender, TappedEventArgs e)
    {
        MopupService.Instance.PushAsync(new QrPopUpPage(houseDetailsViewModel.House));
    }
}