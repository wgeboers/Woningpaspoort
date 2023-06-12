using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using System.Threading;
using Woningpaspoort.Model;
using Woningpaspoort.Services;
using Woningpaspoort.ViewModel;
using ZXing;

namespace Woningpaspoort.View.Mobile;

public partial class QrScanPage : ContentPage
{
    HousesViewModel housesViewModel;
	string barcodeResult;
	House house;

    ILocalizationResourceManager _localizationResourceManager;

    public QrScanPage(HousesViewModel housesViewModel, ILocalizationResourceManager localizationResourceManager)
	{
		InitializeComponent();
		this.housesViewModel = housesViewModel;
        _localizationResourceManager = localizationResourceManager;
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
		cameraView.Camera = cameraView.Cameras.First();

		MainThread.BeginInvokeOnMainThread(async () =>
		{
            await cameraView.StopCameraAsync();
			await cameraView.StartCameraAsync();
		});
    }

    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        MainThread.BeginInvokeOnMainThread(async () =>
		{
			barcodeResult = args.Result[0].Text;

            if (accessType == NetworkAccess.Internet)
            {
                _ = housesViewModel.GetHouseByCode(barcodeResult);
                house = housesViewModel.house;
                if (house == null)
                {
                    return;
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(HouseDetailsPage), true, new Dictionary<string, object>
                    {
                        { "House", house }
                    });
                }
            }
            else
            {
                var toast = Toast.Make(_localizationResourceManager["NoInternet"], ToastDuration.Short, 14);
                await toast.Show(cancellationTokenSource.Token);
            }
        });
    }
}