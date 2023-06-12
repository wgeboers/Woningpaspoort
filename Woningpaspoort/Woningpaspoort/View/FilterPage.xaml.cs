using Woningpaspoort.Model;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort.View.Mobile;

public partial class FilterPage : ContentPage
{
	HousesViewModel housesViewModel;
	public FilterPage(HousesViewModel housesViewModel)
	{
		InitializeComponent();
		this.housesViewModel = housesViewModel;

        var currentCustomers = housesViewModel.GetCustomers();

        if (currentCustomers != null)
        {
            foreach (string customer in currentCustomers)
            {
                customerPicker.Items.Add(customer);
            }
        }

        BindingContext = housesViewModel;
    }

    private async void TapGestureRecognizer_SubmitFilter(object sender, TappedEventArgs e)
    {
        List<House> filteredHouses = housesViewModel.Houses.ToList();

        if (streetEntry.Text != null)
		    housesViewModel.StreetFilter = streetEntry.Text.ToLowerInvariant();
        if (numberEntry.Text != null)
            housesViewModel.NumberFilter = numberEntry.Text.ToLowerInvariant();
        if (additionEntry.Text != null)
            housesViewModel.AdditionFilter = additionEntry.Text.ToLowerInvariant();
        if (zipcodeEntry.Text != null)
            housesViewModel.ZipcodeFilter = zipcodeEntry.Text.ToLowerInvariant();
        if (cityEntry.Text != null)
            housesViewModel.CityFilter = cityEntry.Text.ToLowerInvariant();
        if (countryEntry.Text != null)
            housesViewModel.CountryFilter = countryEntry.Text.ToLowerInvariant();
        if (customerPicker.SelectedItem != null)
            housesViewModel.CustomerFilter = customerPicker.SelectedItem.ToString().ToLowerInvariant();
        if (fromYearEntry.Text != null)
            housesViewModel.YearFromFilter = int.Parse(fromYearEntry.Text);
        if (tillYearEntry.Text != null)
            housesViewModel.YearTillFilter = int.Parse(tillYearEntry.Text);
        if (daebEntry.IsToggled)
            housesViewModel.DaebFilter = daebEntry.IsToggled;

        if (housesViewModel.StreetFilter != null)
            filteredHouses = filteredHouses.Where(a => a.Street.ToLowerInvariant().Contains(housesViewModel.StreetFilter)).ToList();

        if (housesViewModel.NumberFilter != null)
            filteredHouses = filteredHouses.Where(a => a.Number.ToString().ToLowerInvariant().Contains(housesViewModel.NumberFilter)).ToList();

        if (housesViewModel.AdditionFilter != null)
            filteredHouses = filteredHouses.Where(a => a.Addition != null).Where(a => a.Addition.ToLowerInvariant().Contains(housesViewModel.AdditionFilter)).ToList();

        if (housesViewModel.ZipcodeFilter != null)
            filteredHouses = filteredHouses.Where(a => a.ZipCode.ToLowerInvariant().Contains(housesViewModel.ZipcodeFilter)).ToList();

        if (housesViewModel.CityFilter != null)
            filteredHouses = filteredHouses.Where(a => a.City.ToLowerInvariant().Contains(housesViewModel.CityFilter)).ToList();

        if (housesViewModel.CountryFilter != null)
            filteredHouses = filteredHouses.Where(a => a.Country.ToLowerInvariant().Contains(housesViewModel.CountryFilter)).ToList();

        if (housesViewModel.CustomerFilter != null)
            filteredHouses = filteredHouses.Where(a => a.Customer.ToLowerInvariant().Contains(housesViewModel.CustomerFilter)).ToList();

        if (housesViewModel.YearFromFilter != 0 && housesViewModel.YearTillFilter != 0)
        {
            filteredHouses = filteredHouses.Where(a => a.YearBuild != null).Where(
                a => a.YearBuild >= housesViewModel.YearFromFilter  && a.YearBuild <= housesViewModel.YearTillFilter).ToList();
        }

        if (housesViewModel.DaebFilter)
            filteredHouses = filteredHouses.Where(a => a.Daeb.Equals(housesViewModel.DaebFilter)).ToList();


        if (filteredHouses.Count != 0)
		{
            foreach (var house in housesViewModel.Houses.ToList())
            {
                if (!filteredHouses.Contains(house))
                {
                    housesViewModel.Houses.Remove(house);
                }
            }
        }
        else
        {
            housesViewModel.Houses.Clear();
        }

        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private async void TapGestureRecognizer_ClearFilter(object sender, TappedEventArgs e)
    {
        housesViewModel.StreetFilter = null;
        housesViewModel.NumberFilter = null;
        housesViewModel.AdditionFilter = null;
        housesViewModel.ZipcodeFilter = null;
        housesViewModel.CityFilter = null;
        housesViewModel.CountryFilter = null;
        housesViewModel.CustomerFilter = null;
        housesViewModel.YearFromFilter = 0;
        housesViewModel.YearTillFilter = 0;
        housesViewModel.DaebFilter = false;

        housesViewModel.GetHousesCommand.Execute(null);
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private async void TapGestureRecognizer_NavigateMainPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }
}