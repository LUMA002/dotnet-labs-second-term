using FluentValidation;
using Labs.Application.DTOs.Request;

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
            .Length(2, 100).WithMessage("First name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Zà-ÿÀ-ß³²¿¯ºª´¥\s'-]+$")
            .WithMessage("First name contains invalid characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 100).WithMessage("Last name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Zà-ÿÀ-ß³²¿¯ºª´¥\s'-]+$")
            .WithMessage("Last name contains invalid characters");

        RuleFor(x => x.MiddleName)
            .Length(2, 100)
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName))
            .WithMessage("Middle name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Zà-ÿÀ-ß³²¿¯ºª´¥\s'-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName))
            .WithMessage("Middle name contains invalid characters");

        RuleFor(x => x.Address)
            .MaximumLength(255)
            .WithMessage("Address is too long (max 255 characters)");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,17}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone format. Expected: +380XXXXXXXXX");
    }
}