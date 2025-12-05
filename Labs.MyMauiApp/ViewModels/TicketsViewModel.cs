using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labs.Application.DTOs.Response;
using Labs.MyMauiApp.Services;
using System.Collections.ObjectModel;

namespace Labs.MyMauiApp.ViewModels;

/// <summary>
/// ViewModel for tickets list page
/// </summary>
public partial class TicketsViewModel : ObservableObject
{
    private readonly TicketApiService _ticketService;

    public TicketsViewModel(TicketApiService ticketService)
    {
        _ticketService = ticketService;
    }

    [ObservableProperty]
    private ObservableCollection<TicketInfoResponseDto> _tickets = [];

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    /// <summary>
    /// Load tickets from API
    /// </summary>
    [RelayCommand]
    private async Task LoadTicketsAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            // HTTP GET /api/tickets
            var data = await _ticketService.GetAllTicketsAsync();
            Tickets = new ObservableCollection<TicketInfoResponseDto>(data);
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = "Unable to connect to server. Please check your internet connection.";
            System.Diagnostics.Debug.WriteLine($"HTTP Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading tickets: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Error: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Pull-to-refresh
    /// </summary>
    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;
        await LoadTicketsAsync();
        IsRefreshing = false;
    }
}