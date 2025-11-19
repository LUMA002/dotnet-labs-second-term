using Labs.Application.DTOs.Request;

namespace Labs.Blazor.Models;

/// <summary>
/// Mutable ViewModel for Blazor views (create passengers)
/// and two-way binding in EditForm on client side.
/// Validation is handled by FluentValidation on DTO level.
/// </summary>
public class CreatePassengerViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Convert to immutable DTO for service layer
    /// </summary>
    public CreatePassengerRequestDto ToDto() => new(
        FirstName,
        LastName,
        MiddleName,
        Address,
        PhoneNumber
    );
}