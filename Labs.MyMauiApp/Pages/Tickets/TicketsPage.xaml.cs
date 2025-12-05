using Labs.MyMauiApp.ViewModels;

namespace Labs.MyMauiApp.Pages.Tickets;

public partial class TicketsPage : ContentPage
{
    private readonly TicketsViewModel _viewModel;

    public TicketsPage(TicketsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadTicketsCommand.ExecuteAsync(null);
    }
}