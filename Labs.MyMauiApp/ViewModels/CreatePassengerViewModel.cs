using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labs.Application.DTOs.Request;
using Labs.MyMauiApp.Services;
using Labs.MyMauiApp.Services.ValidatorServices;

namespace Labs.MyMauiApp.ViewModels;

/// <summary>
/// ViewModel for creating new passenger
/// </summary>
public partial class CreatePassengerViewModel : ObservableObject
{
    private readonly PassengerApiService _passengerService;
    private readonly INavigationService _navigationService;
    private readonly IErrorHandlingService _errorHandler;

    public CreatePassengerViewModel(
        PassengerApiService passengerService,
        INavigationService navigationService,
        IErrorHandlingService errorHandler)
    {
        _passengerService = passengerService;
        _navigationService = navigationService;
        _errorHandler = errorHandler;
    }

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
    private bool _isSaving;

    [ObservableProperty]
    private Dictionary<string, string> _validationErrors = new();

    public bool HasErrors => ValidationErrors.Count > 0;

    /// <summary>
    /// Validate all inputs (client-side validation for UX)
    /// </summary>
    private bool ValidateInputs()
    {
        ValidationErrors = PassengerValidatorService.Validate(
            FirstName,
            LastName,
            MiddleName,
            PhoneNumber,
            Address);

        return !HasErrors;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsSaving) return;

        // Client-side validation
        if (!ValidateInputs())
        {
            var errors = string.Join("\n", ValidationErrors.Values);
            await _navigationService.DisplayAlertAsync("Validation Error", errors, "OK");
            return;
        }

        try
        {
            IsSaving = true;

            var dto = new CreatePassengerRequestDto(
                FirstName.Trim(),
                LastName.Trim(),
                string.IsNullOrWhiteSpace(MiddleName) ? null : MiddleName.Trim(),
                string.IsNullOrWhiteSpace(Address) ? null : Address.Trim(),
                PhoneNumber?.Trim()
            );

            var result = await _passengerService.CreateAsync(dto);

            if (result != null)
            {
                await _navigationService.DisplayAlertAsync("Success", "Passenger created successfully", "OK");
                await _navigationService.NavigateToAsync("///passengers");
            }
        }
        catch (Exception ex)
        {
            await _errorHandler.HandleErrorAsync(ex, "Creating passenger");
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