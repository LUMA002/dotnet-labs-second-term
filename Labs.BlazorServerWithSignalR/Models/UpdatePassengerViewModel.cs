using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;

namespace Labs.BlazorServerWithSignalR.Models;

/// <summary>
/// Mutable ViewModel for Blazor forms of editing passengers.
/// Validation is handled by FluentValidation on DTO level.
/// </summary>
public class UpdatePassengerViewModel
{
    public Guid PassengerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    public UpdatePassengerRequestDto ToDto() => new(
        PassengerId,
        FirstName,
        LastName,
        MiddleName,
        Address,
        PhoneNumber
    );

    public static UpdatePassengerViewModel FromDto(PassengerResponseDto dto) => new()
    {
        PassengerId = dto.Id,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        MiddleName = dto.MiddleName,
        Address = dto.Address,
        PhoneNumber = dto.PhoneNumber
    };
}