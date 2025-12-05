using Labs.MyMauiApp.ViewModels;

namespace Labs.MyMauiApp.Pages.Passengers;

public partial class PassengersPage : ContentPage
{
    private readonly PassengersViewModel _viewModel;

    public PassengersPage(PassengersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPassengersCommand.ExecuteAsync(null);
    }
}