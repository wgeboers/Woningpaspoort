namespace Woningpaspoort.View.Mobile;
using CommunityToolkit.Maui.Storage;

using System.Threading;
using Woningpaspoort.ViewModel;
using Image = Woningpaspoort.Model.Image;

public partial class OpenImagePopUpPage
{
    Image image;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    HouseDetailsViewModel houseDetailsViewModel;
    public OpenImagePopUpPage(Image image, HouseDetailsViewModel houseDetailsViewModel)
	{
		InitializeComponent();
        this.image = image;
        this.houseDetailsViewModel = houseDetailsViewModel;

        imagePopUp.Source = image.ExternalURL;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            imagePopUp.HeightRequest = 300;
            imagePopUp.WidthRequest = 500;
        }
        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        {
            imagePopUp.HeightRequest = 200;
            imagePopUp.WidthRequest = 250;
        }
    }

    private async void TapGestureRecognizer_DownloadImage(object sender, TappedEventArgs e)
    {
        var fileExtension = Path.GetExtension(image.URL);
        var fileName = Path.GetRandomFileName().ToLowerInvariant() + fileExtension;

        var stream = await houseDetailsViewModel.GetImage(image.ExternalURL);

        await FileSaver.Default.SaveAsync(fileName, stream, cancellationTokenSource.Token);
    }
}