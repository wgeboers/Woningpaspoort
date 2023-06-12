using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LocalizationResourceManager.Maui;
using Woningpaspoort.ViewModel;

namespace Woningpaspoort.View.Mobile;

public partial class WorkPage : ContentPage
{
    ILocalizationResourceManager _localizationResourceManager;
    HouseDetailsViewModel houseDetailsViewModel;
    public WorkPage(HouseDetailsViewModel houseDetailsViewModel, ILocalizationResourceManager localizationResourceManager)
    {
        InitializeComponent();
        _localizationResourceManager = localizationResourceManager;
        this.houseDetailsViewModel = houseDetailsViewModel;
        BindingContext = houseDetailsViewModel;

        if (DeviceInfo.Platform == DevicePlatform.WinUI)
            WorkPageCollectionView.ItemsLayout = new GridItemsLayout(4, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 10,
                HorizontalItemSpacing = 10
            };
    }
}