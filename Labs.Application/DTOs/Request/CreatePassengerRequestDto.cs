namespace Labs.Application.DTOs.Request;

public record CreatePassengerRequestDto(
    string? FirstName,
    string? LastName,
    string? MiddleName,
    string? Address,
    string? PhoneNumber
);