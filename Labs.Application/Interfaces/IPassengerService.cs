using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;

namespace Labs.Application.Interfaces;

/// <summary>
/// Interface for passenger CRUD operations
/// Used for Decorator pattern in Blazor with SignalR
/// </summary>
public interface IPassengerService
{
    Task<IEnumerable<PassengerResponseDto>> GetAllPassengersAsync();
    Task<PassengerResponseDto?> GetPassengerByIdAsync(Guid id);
    Task<PassengerResponseDto> CreatePassengerAsync(CreatePassengerRequestDto dto);
    Task<bool> UpdatePassengerAsync(UpdatePassengerRequestDto dto);
    Task<bool> DeletePassengerAsync(Guid id);
}