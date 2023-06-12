using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Maps;
using System.Diagnostics;

namespace Woningpaspoort.ViewModel;

public partial class MapsViewModel : BaseViewModel
{
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    [ObservableProperty]
    public double latitude;

    [ObservableProperty]
    public double longitude;

    public MapsViewModel()
	{
        _ = SpecificMapLocationAsync();
	}

    async Task SpecificMapLocationAsync()
    {
        try
        {
            _isCheckingLocation = true;
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get current location: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }
}