namespace Labs.Application.DTOs.Response;
/// <summary>
/// DTO for returning passenger data
/// </summary>
public record PassengerResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? Address,
    string? PhoneNumber
);