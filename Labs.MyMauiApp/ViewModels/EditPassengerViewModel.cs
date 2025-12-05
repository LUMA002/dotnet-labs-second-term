using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labs.Application.DTOs.Request;
using Labs.MyMauiApp.Services;
using Labs.MyMauiApp.Services.ValidatorServices;

namespace Labs.MyMauiApp.ViewModels;

/// <summary>
/// ViewModel for editing passenger
/// </summary>
[QueryProperty(nameof(PassengerIdString), "id")]
public partial class EditPassengerViewModel : ObservableObject
{
    private readonly PassengerApiService _passengerService;
    private readonly INavigationService _navigationService;
    private readonly IErrorHandlingService _errorHandler;

    public EditPassengerViewModel(
        PassengerApiService passengerService,
        INavigationService navigationService,
        IErrorHandlingService errorHandler)
    {
        _passengerService = passengerService;
        _navigationService = navigationService;
        _errorHandler = errorHandler;
    }

    // Accept string from route, convert to Guid
    private string _passengerIdString = string.Empty;
    public string PassengerIdString
    {
        get => _passengerIdString;
        set
        {
            _passengerIdString = value;
            if (Guid.TryParse(value, out var guid))
            {
                PassengerId = guid;
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"[EditPassengerViewModel] ID set: {PassengerId}");
#endif
                Task.Run(async () => await LoadPassengerAsync());
            }
#if DEBUG
            else
            {
                System.Diagnostics.Debug.WriteLine($"[EditPassengerViewModel] Invalid ID: {value}");
            }
#endif
        }
    }

    [ObservableProperty]
    private Guid _passengerId;

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string? _middleName;

    [ObservableProperty]
    private string? _address;

    [ObservableProperty]
    private string? _phoneNumber;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isSaving;

    [RelayCommand]
    private async Task LoadPassengerAsync()
    {
        if (PassengerId == Guid.Empty)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[EditPassengerViewModel] PassengerId is empty");
#endif
            return;
        }

        try
        {
            IsLoading = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[EditPassengerViewModel] Loading passenger: {PassengerId}");
#endif

            var passenger = await _passengerService.GetByIdAsync(PassengerId);

            if (passenger != null)
            {
                FirstName = passenger.FirstName;
                LastName = passenger.LastName;
                MiddleName = passenger.MiddleName;
                Address = passenger.Address;
                PhoneNumber = passenger.PhoneNumber;

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"[EditPassengerViewModel] Loaded: {FirstName} {LastName}");
#endif
            }
            else
            {
                await _navigationService.DisplayAlertAsync("Error", "Passenger not found", "OK");
                await _navigationService.GoBackAsync();
            }
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Loading passenger");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsSaving) return;

        var errors = PassengerValidatorService.Validate(FirstName, LastName, MiddleName, PhoneNumber, Address);

        if (errors.Count > 0)
        {
            var errorMessage = string.Join("\n", errors.Values);
            await _navigationService.DisplayAlertAsync("Validation Error", errorMessage, "OK");
            return;
        }

        try
        {
            IsSaving = true;

            var dto = new UpdatePassengerRequestDto(
                PassengerId,
                FirstName.Trim(),
                LastName.Trim(),
                string.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName.Trim(),
                string.IsNullOrWhiteSpace(Address) ? null : Address.Trim(),
                PhoneNumber?.Trim()
            );

            var success = await _passengerService.UpdateAsync(PassengerId, dto);

            if (success)
            {
                await _navigationService.DisplayAlertAsync("Success", "Passenger updated successfully", "OK");
                await _navigationService.NavigateToAsync("///passengers");
            }
            else
            {
                await _navigationService.DisplayAlertAsync("Error", "Failed to update passenger", "OK");
            }
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Updating passenger");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await _navigationService.GoBackAsync();
    }
}