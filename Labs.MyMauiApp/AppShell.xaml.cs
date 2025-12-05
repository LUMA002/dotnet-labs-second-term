using Labs.MyMauiApp.Pages.Passengers;

namespace Labs.MyMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("passengers/create", typeof(CreatePassengerPage));
        Routing.RegisterRoute("passengers/edit", typeof(EditPassengerPage));
    }
}