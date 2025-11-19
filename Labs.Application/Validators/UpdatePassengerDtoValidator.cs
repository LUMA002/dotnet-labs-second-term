using FluentValidation;
using Labs.Application.DTOs.Request;
using Labs.Domain.Constants;

namespace Labs.Application.Validators;

/// <summary>
/// Validator for UpdatePassengerDto
/// Inherits validation rules from CreatePassengerDtoValidator
/// </summary>
public class UpdatePassengerDtoValidator : AbstractValidator<UpdatePassengerRequestDto>
{
    public UpdatePassengerDtoValidator()
    {
        RuleFor(x => x.PassengerId)
            .NotEmpty().WithMessage("Passenger ID is required");

        // Same rules as CreatePassengerDto
        RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("First name is required")
             .Length(2, ModelConstants.PassengerNameMaxLength)
             .WithMessage($"First name must be between 2 and {ModelConstants.PassengerNameMaxLength} characters")
             .Matches(@"^[a-zA-ZÀ-ßà-ÿ²³¯¿ªº¥´\s'-]+$")
             .WithMessage("First name contains invalid characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, ModelConstants.PassengerNameMaxLength)
            .WithMessage($"Last name must be between 2 and {ModelConstants.PassengerNameMaxLength} characters")
            .Matches(@"^[a-zA-ZÀ-ßà-ÿ²³¯¿ªº¥´\s'-]+$")
            .WithMessage("Last name contains invalid characters");

        RuleFor(x => x.MiddleName)
            .Length(2, ModelConstants.PassengerNameMaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName))
            .WithMessage($"Middle name must be between 2 and {ModelConstants.PassengerNameMaxLength} characters")
            .Matches(@"^[a-zA-ZÀ-ßà-ÿ²³¯¿ªº¥´\s'-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName))
            .WithMessage("Middle name contains invalid characters");

        RuleFor(x => x.Address)
            .MaximumLength(ModelConstants.PassengerAddressMaxLength)
            .WithMessage($"Address cannot exceed {ModelConstants.PassengerAddressMaxLength} characters");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(ModelConstants.PassengerPhoneMaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage($"Phone number cannot exceed {ModelConstants.PassengerPhoneMaxLength} characters")
            .Matches(@"^\+?[0-9]{10,17}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone format. Expected: +380XXXXXXXXX or 10-17 digits");
    }
}