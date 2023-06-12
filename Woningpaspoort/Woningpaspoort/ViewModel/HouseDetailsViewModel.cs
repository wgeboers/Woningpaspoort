using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Woningpaspoort.Model;
using Woningpaspoort.Services;

namespace Woningpaspoort.ViewModel;

[QueryProperty(nameof(House), "House")]
public partial class HouseDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    House house;
    HouseService houseService;

    public HouseDetailsViewModel(HouseService houseService)
	{
        this.houseService = houseService;
    }

    public async Task<Stream> GetImage(string externalURL)
    {
        var image = await houseService.GetImage(externalURL);

        return image;
    }
}
