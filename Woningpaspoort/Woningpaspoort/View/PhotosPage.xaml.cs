using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using LocalizationResourceManager.Maui;
using Mopups.Services;
using System.Text;
using Woningpaspoort.Services;
using Woningpaspoort.ViewModel;
using Image = Woningpaspoort.Model.Image;

namespace Woningpaspoort.View.Mobile;

public partial class PhotosPage : ContentPage
{
    ILocalizationResourceManager _localizationResourceManager;
    ImageService imageService;
    HouseDetailsViewModel houseDetailsViewModel;
	public PhotosPage(HouseDetailsViewModel houseDetailsViewModel, ILocalizationResourceManager localizationResourceManager, ImageService imageService)
	{
		InitializeComponent();
        BindingContext = houseDetailsViewModel;
        _localizationResourceManager = localizationResourceManager;
        this.imageService = imageService;
        this.houseDetailsViewModel = houseDetailsViewModel;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
            PhotoPageCollectionView.ItemsLayout = new GridItemsLayout(8, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 10,
                HorizontalItemSpacing = 10
            };
    }

    private async void TapGestureRecognizer_OpenImage(object sender, TappedEventArgs e)
    {
        var client = new HttpClient();
        var image = ((VisualElement)sender).BindingContext as Image;

        if (image == null)
            return;

        MopupService.Instance.PushAsync(new OpenImagePopUpPage(image, houseDetailsViewModel));
    }

    private async void TapGestureRecognizer_AddImage(object sender, TappedEventArgs e)
    {
        if (houseDetailsViewModel.House != null)
        {
            MopupService.Instance.PushAsync(new AddImagePopUpPage(_localizationResourceManager, imageService, houseDetailsViewModel.House));
        }
        else
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = _localizationResourceManager["NoHouseSelected"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}