using System.Collections.ObjectModel;
using System.Diagnostics;
using Woningpaspoort.Model;
using Woningpaspoort.Services;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls;

namespace Woningpaspoort.View.Mobile;

public partial class MapsPage : ContentPage
{
    HouseService houseService;

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    public MapsPage(HouseService houseService)
	{
        this.houseService = houseService;
        _ = SpecificMapLocationAsync();
        _ = FillPinsAsync();
        InitializeComponent();
    }

    private async void TapGestureRecognizer_NavigateMainPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    async Task FillPinsAsync()
    {
        try
        {
            var houses = await houseService.GetHouses();
            var showableHouses = houses.Where(b => b.Latitude != null || b.Longitude != null);

            foreach (var house in showableHouses)
            {
                Pin pin = new()
                {
                    Label = house.Code,
                    Address = house.Street + " " + house.Number + house.Addition,
                    Type = PinType.Place,
                    Location = new Location((double)house.Latitude, (double)house.Longitude)
                };

                map.Pins.Add(pin);
                //pin.InfoWindowClicked += async (s, args) =>
                //{
                //    await Shell.Current.GoToAsync(nameof(HouseDetailsPage), true, new Dictionary<string, object>
                //    {
                //        { "House", house }
                //    });
                //};
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get houses and create pins: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    async Task SpecificMapLocationAsync()
    {
        try
        {
            _isCheckingLocation = true;
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
            MapSpan mapSpan = new MapSpan(location, 0.5, 0.5);

            map.MoveToRegion(mapSpan);
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