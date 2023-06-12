using Mopups.Services;

namespace Woningpaspoort.View.Mobile;

public partial class ManagePortalPage : ContentPage
{
	public ManagePortalPage()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_NavigateMainPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }

    private async void TapGestureRecognizer_AddHousePopUp(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddHousePage));
    }
}