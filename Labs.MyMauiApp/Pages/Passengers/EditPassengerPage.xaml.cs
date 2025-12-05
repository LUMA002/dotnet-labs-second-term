using Labs.MyMauiApp.ViewModels;

namespace Labs.MyMauiApp.Pages.Passengers;

public partial class EditPassengerPage : ContentPage
{
    public EditPassengerPage(EditPassengerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}