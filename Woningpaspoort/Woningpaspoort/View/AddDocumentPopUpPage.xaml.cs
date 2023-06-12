using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using Woningpaspoort.Model;
using Woningpaspoort.Services;

namespace Woningpaspoort.View.Mobile;

public partial class AddDocumentPopUpPage
{
	ILocalizationResourceManager _localizationResourceManager;
	DocumentService documentService;
	House house;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public AddDocumentPopUpPage(ILocalizationResourceManager localizationResourceManager, DocumentService documentService, House house)
	{
		InitializeComponent();
        _localizationResourceManager = localizationResourceManager;
		this.documentService = documentService;
		this.house = house;
    }

    private async void TapGestureRecognizer_ChooseDocument(object sender, TappedEventArgs e)
    {
        string description;

        if (descriptionEntry.Text != null)
        {
            description = descriptionEntry.Text;

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = _localizationResourceManager["PickDocument"],
                FileTypes = FilePickerFileType.Pdf
            });

            if (result == null)
                return;

            var responseDocument = await documentService.PostDocument(description, "wgeboers", result);
            await documentService.PostHouseDocument(house.ObjectId, responseDocument.DocumentId);

            string text = _localizationResourceManager["DocumentCreated"];
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