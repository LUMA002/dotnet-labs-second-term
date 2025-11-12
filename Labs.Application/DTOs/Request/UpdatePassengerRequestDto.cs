namespace Labs.Application.DTOs.Request;

/// <summary>
/// DTO for updating existing passenger
/// </summary>
public record UpdatePassengerRequestDto(
    Guid PassengerId, 
    string FirstName,
    string LastName,
    string? MiddleName,
    string? Address,
    string? PhoneNumber
);  