using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using System.Threading;
using Woningpaspoort.Model;
using Woningpaspoort.Services;

namespace Woningpaspoort.View.Mobile;

public partial class AddImagePopUpPage
{
    ILocalizationResourceManager _localizationResourceManager;
    ImageService imageService;
    House house;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public AddImagePopUpPage(ILocalizationResourceManager localizationResourceManager, ImageService imageService, House house)
    {
        InitializeComponent();
        _localizationResourceManager = localizationResourceManager;
        this.imageService = imageService;
        this.house = house;
    }

    private async void TapGestureRecognizer_ChoosePicture(object sender, TappedEventArgs e)
    {
        string description;
        bool thumbnail;

        if (descriptionEntry.Text != null)
        {
            description = descriptionEntry.Text;

            if (thumbnailEntry.IsToggled)
                thumbnail = true;
            else
                thumbnail = false;

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = _localizationResourceManager["PickImage"],
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
                return;

            var responseImage = await imageService.PostImage(description, thumbnail, "wgeboers", result);
            await imageService.PostHouseImage(house.ObjectId, responseImage.ImageId);

            string text = _localizationResourceManager["ImageCreated"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = _localizationResourceManager["NoDescription"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);

            return;
        }
    }
}