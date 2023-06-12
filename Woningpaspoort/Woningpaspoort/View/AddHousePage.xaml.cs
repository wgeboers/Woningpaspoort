using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using Microsoft.Maui.Platform;
using Woningpaspoort.Model;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort.View.Mobile;

public partial class AddHousePage : ContentPage
{
    HousesViewModel housesViewModel;
    ILocalizationResourceManager _localizationResourceManager;
    public AddHousePage(HousesViewModel housesViewModel, ILocalizationResourceManager localizationResourceManager)
	{
		InitializeComponent();
        this.housesViewModel = housesViewModel;
        _localizationResourceManager = localizationResourceManager;

        var currentCustomers = housesViewModel.GetCustomers();

        if (currentCustomers != null)
        {
            foreach (string customer in currentCustomers)
            {
                customerPicker.Items.Add(customer);
            }
        }
    }

    private async void TapGestureRecognizer_NavigateBeheerPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManagePortalPage));
    }

    private async void TapGestureRecognizer_SubmitHouse(object sender, TappedEventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        string street = null;
        int number = 0;
        string addition = null;
        string zipcode = null;
        string city = null;
        string county = null;
        string customer = null;
        string externalcode = null;
        int buildYear = 0;
        bool daeb = false;
        string createdBy = "wgeboers";

        if (streetEntry.Text != null)
        {
            street = streetEntry.Text.Trim();
        }
        else
        {
            var streetToast = Toast.Make(_localizationResourceManager["StreetEmpty"], ToastDuration.Short, 14);
            await streetToast.Show(cancellationTokenSource.Token);

            return;
        }
            
        if (numberEntry.Text != null)
        {
            try
            {
                number = int.Parse(numberEntry.Text.Trim());
            }
            catch
            {
                var numberToast = Toast.Make(_localizationResourceManager["NumberInputNotSupported"], ToastDuration.Short, 14);
                await numberToast.Show(cancellationTokenSource.Token);

                return;
            }
        }
        else
        {
            var numberToast = Toast.Make(_localizationResourceManager["NumberEmpty"], ToastDuration.Short, 14);
            await numberToast.Show(cancellationTokenSource.Token);

            return;
        }
            
        if (additionEntry.Text != null)
            addition = additionEntry.Text.Trim();

        if (zipcodeEntry.Text != null)
        {
            zipcode = zipcodeEntry.Text.ToUpperInvariant().Replace(" ", "");
        }
        else
        {
            var zipCodeToast = Toast.Make(_localizationResourceManager["ZipCodeEmpty"], ToastDuration.Short, 14);
            await zipCodeToast.Show(cancellationTokenSource.Token);

            return;
        }
            
        if (cityEntry.Text != null)
        {
            city = cityEntry.Text.Trim();
        }
        else
        {
            var cityToast = Toast.Make(_localizationResourceManager["CityEmpty"], ToastDuration.Short, 14);
            await cityToast.Show(cancellationTokenSource.Token);

            return;
        }
            

        if (countryEntry.Text != null)
        {
            county = countryEntry.Text.Trim();
        }
        else
        {
            var countryToast = Toast.Make(_localizationResourceManager["CountryEmpty"], ToastDuration.Short, 14);
            await countryToast.Show(cancellationTokenSource.Token);

            return;
        }

        if (customerPicker.SelectedItem != null)
        {
            customer = customerPicker.SelectedItem.ToString();
        }
        else
        {
            var customerToast = Toast.Make(_localizationResourceManager["CustomerEmpty"], ToastDuration.Short, 14);
            await customerToast.Show(cancellationTokenSource.Token);

            return;
        }
            
        if (externalCodeEntry.Text != null)
            externalcode = externalCodeEntry.Text.Trim();

        if (buildYearEntry.Text != null)
            buildYear = int.Parse(buildYearEntry.Text.Trim());

        if (daebEntry.IsToggled)
            daeb = daebEntry.IsToggled;

        if (accessType == NetworkAccess.Internet)
        {
            await housesViewModel.PostHouseAsync(street, number, addition, zipcode, city, county, externalcode, customer, buildYear, daeb, createdBy);

            _localizationResourceManager.CurrentCulture = new System.Globalization.CultureInfo("nl");

            string text = _localizationResourceManager["HouseCreated"];
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);

            await Shell.Current.GoToAsync(nameof(ManagePortalPage));
        }
        else
        {
            var toast = Toast.Make(_localizationResourceManager["NoInternet"], ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}