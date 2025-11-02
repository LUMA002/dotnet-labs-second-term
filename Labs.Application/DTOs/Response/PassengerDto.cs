namespace Labs.Application.DTOs.Response;

public record PassengerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? MiddleName,
    string? Address,
    string? PhoneNumber
);