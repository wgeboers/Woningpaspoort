using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Woningpaspoort.Model;
using Woningpaspoort.Services;

namespace Woningpaspoort.ViewModel;

public partial class HousesViewModel : BaseViewModel
{
	[ObservableProperty]
	bool isRefreshing;
	public ObservableCollection<House> Houses { get; set; } = new();
	public House house { get; set; }
	public Command GetHousesCommand { get; }
	HouseService houseService;

    [ObservableProperty]
    string streetFilter;
    [ObservableProperty]
    string numberFilter;
    [ObservableProperty]
    string additionFilter;
    [ObservableProperty]
    string zipcodeFilter;
    [ObservableProperty]
    string cityFilter;
    [ObservableProperty]
    string countryFilter;
    [ObservableProperty]
    string customerFilter;
    [ObservableProperty]
    int yearFromFilter;
    [ObservableProperty]
    int yearTillFilter;
    [ObservableProperty]
    bool daebFilter;

    public HousesViewModel(HouseService houseService)
	{
		this.houseService = houseService;
        _ = GetHousesAsync();
        GetHousesCommand = new Command(async () => await GetHousesAsync());
    }

    async Task GetHousesAsync()
	{
        if (IsBusy)
			return;

        //if (accessType == NetworkAccess.Internet)
        //{
            try
            {
                IsBusy = true;

                var houses = await houseService.GetHouses();

                if (Houses.Count != 0)
                    Houses.Clear();

                foreach (var house in houses)
                {
                    Houses.Add(house);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get houses: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }

            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        //}
        //else
        //{
        //    var toast = Toast.Make(_localizationResourceManager["NoInternet"], ToastDuration.Short, 14);
        //    await toast.Show(cancellationTokenSource.Token);
        //}
	}

	public List<string> GetCustomers()
	{
		List<string> customer = new List<string>();

		if (Houses != null)
		{
			foreach (House house in Houses)
			{
				if (customer.Contains(house.Customer))
				{
					continue;
				}
				else
				{
                    customer.Add(house.Customer);
                }
			}
		}

		return customer;
	}

	public async Task GetHouseByCode(string code)
	{
		house = await houseService.GetHouseByCode(code);
	}

	public async Task PostHouseAsync(string street, int number, string addition, string zipcode, string city, string country, string externalcode, string customer, int yearBuild, bool daeb, string createdBy)
	{
		var house = await houseService.PostHouse(street, number, addition, zipcode, city, country, externalcode, customer, yearBuild, daeb, createdBy);
	}
}