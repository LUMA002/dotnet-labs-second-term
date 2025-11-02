namespace Labs.Application.DTOs.Request;

public record CreatePassengerDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string? Address,
    string? PhoneNumber
);