using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using Woningpaspoort.Model;
using Woningpaspoort.View.Controls;
using Woningpaspoort.View.Mobile;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort;

public partial class MainPage : ContentPage
{
    ILocalizationResourceManager _localizationResourceManager;
    HousesViewModel housesViewModel;
	public MainPage(HousesViewModel housesViewModel, ILocalizationResourceManager localizationResourceManager)
	{
		InitializeComponent();
        this.housesViewModel = housesViewModel;
		BindingContext = housesViewModel;

        _localizationResourceManager = localizationResourceManager;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            MainPageCollectionView.ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 10,
                HorizontalItemSpacing = 10
            };

            NavigateMapsButton.IsVisible = false;
        }
        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        {
            RefreshButton.IsVisible = false;
        }
    }

    private async void TapGestureRecognizer_HouseDetails(object sender, TappedEventArgs e)
    {
		var house = ((VisualElement)sender).BindingContext as House;

		if (house == null)
			return;

		await Shell.Current.GoToAsync(nameof(HouseDetailsPage), true, new Dictionary<string, object>
		{
			{ "House", house }
		});
    }

    private async void TapGestureRecognizer_NavigateMapsPage(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(MapsPage));
    }

    private async void TapGestureRecognizer_NavigateFilterPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilterPage));
    }

    private async void TapGestureRecognizer_Language(object sender, TappedEventArgs e)
    {
        if (_localizationResourceManager.CurrentCulture.TwoLetterISOLanguageName == "nl")
        {
            _localizationResourceManager.CurrentCulture = new System.Globalization.CultureInfo("en");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = _localizationResourceManager["LanguageChangedToEN"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            _localizationResourceManager.CurrentCulture = new System.Globalization.CultureInfo("nl");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = _localizationResourceManager["LanguageChangedToNL"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
            
    }

    private async void TapGestureRecognizer_NavigateManagePage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManagePortalPage));
    }

    private async void TapGestureRecognizer_Refresh(object sender, TappedEventArgs e)
    {
        housesViewModel.GetHousesCommand.Execute(null);
    }
}

