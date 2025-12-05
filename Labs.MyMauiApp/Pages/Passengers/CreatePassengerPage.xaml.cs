using Labs.MyMauiApp.ViewModels;

namespace Labs.MyMauiApp.Pages.Passengers;

public partial class CreatePassengerPage : ContentPage
{
    public CreatePassengerPage(CreatePassengerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}