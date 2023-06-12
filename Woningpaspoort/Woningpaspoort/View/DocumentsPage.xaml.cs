using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using LocalizationResourceManager.Maui;
using Mopups.Services;
using System.Text;
using Woningpaspoort.Model;
using Woningpaspoort.Services;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort.View.Mobile;

public partial class DocumentsPage : ContentPage
{
    ILocalizationResourceManager _localizationResourceManager;
    DocumentService documentService;
    HouseDetailsViewModel houseDetailsViewModel;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public DocumentsPage(HouseDetailsViewModel houseDetailsViewModel, ILocalizationResourceManager localizationResourceManager, DocumentService documentService)
	{
		InitializeComponent();
		BindingContext = houseDetailsViewModel;
        _localizationResourceManager = localizationResourceManager;
        this.documentService = documentService;
        this.houseDetailsViewModel = houseDetailsViewModel;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
            DocumentPageCollectionView.ItemsLayout = new GridItemsLayout(8, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 10,
                HorizontalItemSpacing = 10
            };
    }

    private async void TapGestureRecognizer_DownloadDocument(object sender, TappedEventArgs e)
    {
        var client = new HttpClient();
        var document = ((VisualElement)sender).BindingContext as Document;

        if (document == null)
            return;

        var fileExtension = Path.GetExtension(document.URL);
        var fileName = Path.GetRandomFileName().ToLowerInvariant() + fileExtension;
        HttpResponseMessage response = await client.GetAsync(document.ExternalURL);
        var stream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());
        await FileSaver.Default.SaveAsync(fileName, stream, cancellationTokenSource.Token);
    }

    private async void TapGestureRecognizer_AddDocument(object sender, TappedEventArgs e)
    {
        if (houseDetailsViewModel.House != null)
        {
            MopupService.Instance.PushAsync(new AddDocumentPopUpPage(_localizationResourceManager, documentService, houseDetailsViewModel.House));
        }
        else
        {
            string text = _localizationResourceManager["NoHouseSelected"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}