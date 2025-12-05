using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labs.Application.DTOs.Response;
using Labs.MyMauiApp.Services;
using System.Collections.ObjectModel;

namespace Labs.MyMauiApp.ViewModels;

/// <summary>
/// ViewModel for passengers list
/// </summary>
public partial class PassengersViewModel : ObservableObject
{
    private readonly PassengerApiService _passengerService;
    private readonly INavigationService _navigationService;
    private readonly IErrorHandlingService _errorHandler;

    public PassengersViewModel(
        PassengerApiService passengerService,
        INavigationService navigationService,
        IErrorHandlingService errorHandler)
    {
        _passengerService = passengerService;
        _navigationService = navigationService;
        _errorHandler = errorHandler;
    }

    [ObservableProperty]
    private ObservableCollection<PassengerResponseDto> _passengers = [];

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isRefreshing;

    [RelayCommand]
    private async Task LoadPassengersAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("[PassengersViewModel] Loading passengers...");
#endif
            var data = await _passengerService.GetAllAsync();
            Passengers = new ObservableCollection<PassengerResponseDto>(data);

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[PassengersViewModel] Loaded {Passengers.Count} passengers");
#endif
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Loading passengers");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        IsRefreshing = true;
        await LoadPassengersAsync();
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task AddPassenger()
    {
        await _navigationService.NavigateToAsync("///passengers/create");
    }

    [RelayCommand]
    private async Task EditPassenger(PassengerResponseDto passenger)
    {
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"[PassengersViewModel] Editing passenger: {passenger.Id}");
#endif
        await _navigationService.NavigateToAsync($"///passengers/edit?id={passenger.Id}");
    }

    [RelayCommand]
    private async Task DeletePassenger(PassengerResponseDto passenger)
    {
        var confirm = await _navigationService.DisplayConfirmAsync(
            "Delete Passenger",
            $"Are you sure you want to delete {passenger.FirstName} {passenger.LastName}?",
            "Yes", "No");

        if (!confirm) return;

        try
        {
            var success = await _passengerService.DeleteAsync(passenger.Id);

            if (success)
            {
                await LoadPassengersAsync();
                await _navigationService.DisplayAlertAsync("Success", "Passenger deleted successfully", "OK");
            }
            else
            {
                await _navigationService.DisplayAlertAsync("Error", "Failed to delete passenger", "OK");
            }
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Deleting passenger");
        }
    }
}