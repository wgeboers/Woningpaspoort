using SimpleToolkit.Core;
using Woningpaspoort.View.Mobile;

namespace Woningpaspoort;

public partial class AppShell : SimpleToolkit.SimpleShell.SimpleShell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(HouseDetailsPage), typeof(HouseDetailsPage));
        Routing.RegisterRoute(nameof(MapsPage), typeof(MapsPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(FilterPage), typeof(FilterPage));
        Routing.RegisterRoute(nameof(ManagePortalPage), typeof(ManagePortalPage));
        Routing.RegisterRoute(nameof(AddHousePage), typeof(AddHousePage));

        AddTab(typeof(MainPage), PageType.HomePage);
        AddTab(typeof(WorkPage), PageType.WorkPage);
        AddTab(typeof(DocumentsPage), PageType.DocumentsPage);
        AddTab(typeof(PhotosPage), PageType.PhotosPage);
        AddTab(typeof(QrScanPage), PageType.QrScanPage);

        Loaded += AppShellLoaded;
    }

    private static void AppShellLoaded(object sender, EventArgs e)
    {
        var shell = sender as AppShell;

        shell.Window.SubscribeToSafeAreaChanges(safeArea =>
        {
            shell.pageContainer.Margin = safeArea;
            shell.tabBarView.Margin = safeArea;
            shell.bottomBackgroundRectangle.IsVisible = safeArea.Bottom > 0;
            shell.bottomBackgroundRectangle.HeightRequest = safeArea.Bottom;
        });
    }

    private void AddTab(Type page, PageType pageEnum)
    {
        Tab tab = new Tab { Route = pageEnum.ToString(), Title = pageEnum.ToString() };
        tab.Items.Add(new ShellContent { ContentTemplate = new DataTemplate(page) });

        tabBar.Items.Add(tab);
    }

    private void TabBarViewCurrentPageChanged(object sender, TabBarEventArgs e)
    {
        Shell.Current.GoToAsync("///" + e.CurrentPage.ToString());
    }
}

public enum PageType
{
    HomePage, WorkPage, DocumentsPage, PhotosPage, QrScanPage
}
