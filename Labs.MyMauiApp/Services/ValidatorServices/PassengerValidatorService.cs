using System.Text.RegularExpressions;

namespace Labs.MyMauiApp.Services.ValidatorServices;

/// <summary>
/// Client-side validator for passenger data (shared between Create/Edit)
/// </summary>
public static class PassengerValidatorService
{
    /// <summary>
    /// Validate passenger input fields
    /// </summary>
    /// <returns>Dictionary with field errors (empty if valid)</returns>
    public static Dictionary<string, string> Validate(
    string? firstName,
    string? lastName,
    string? middleName,
    string? phoneNumber,
    string? address)
    {
        var errors = new Dictionary<string, string>();

        ValidateName(firstName, "FirstName", errors, required: true);
        ValidateName(lastName, "LastName", errors, required: true);
        ValidateName(middleName, "MiddleName", errors, required: false);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            errors["PhoneNumber"] = "Phone number is required";
        else if (!Regex.IsMatch(phoneNumber, @"^\+?[0-9]{10,17}$"))
            errors["PhoneNumber"] = "Invalid phone format. Expected: +380XXXXXXXXX or 10-17 digits";

        // Address (optional)
        if (!string.IsNullOrWhiteSpace(address) && address.Length > 255)
            errors["Address"] = "Address cannot exceed 255 characters";

        return errors;
    }

    private static void ValidateName(string? value, string fieldName, Dictionary<string, string> errors, bool required)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            if (required)
                errors[fieldName] = $"{fieldName} is required";
            return;
        }

        if (value.Length < 2 || value.Length > 100)
            errors[fieldName] = $"{fieldName} must be between 2 and 100 characters";
        else if (!Regex.IsMatch(value, @"^[a-zA-ZА-Яа-яІіЇїЄєҐґ\s'-]+$"))
            errors[fieldName] = $"{fieldName} contains invalid characters";
    }
}
