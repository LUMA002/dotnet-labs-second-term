using FluentValidation;
using Labs.Application.DTOs.Request;

namespace Labs.Application.Validators;

/// <summary>
/// FluentValidation validator for CreatePassengerDto
/// </summary>
public class CreatePassengerDtoValidator : AbstractValidator<CreatePassengerRequestDto>
{
    public CreatePassengerDtoValidator()
    {
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