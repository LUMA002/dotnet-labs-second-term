using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.BlazorServerWithSignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Labs.BlazorServerWithSignalR.Services;

public sealed class SignalRPassengerServiceDecorator : IPassengerService
{
    private readonly PassengerService _inner;
    private readonly IHubContext<PassengerHub> _hub;

    public SignalRPassengerServiceDecorator(PassengerService inner, IHubContext<PassengerHub> hub)
    {
        _inner = inner;
        _hub = hub;
    }

    public Task<IEnumerable<PassengerResponseDto>> GetAllPassengersAsync() =>
        _inner.GetAllPassengersAsync();

    public Task<PassengerResponseDto?> GetPassengerByIdAsync(Guid id) =>
        _inner.GetPassengerByIdAsync(id);

    public async Task<PassengerResponseDto> CreatePassengerAsync(CreatePassengerRequestDto dto)
    {
        var created = await _inner.CreatePassengerAsync(dto);
        await _hub.Clients.All.SendAsync("PassengerCreated", created);
        return created;
    }

    public async Task<bool> UpdatePassengerAsync(UpdatePassengerRequestDto dto)
    {
        var ok = await _inner.UpdatePassengerAsync(dto);
        if (ok)
        {
            var updated = await _inner.GetPassengerByIdAsync(dto.PassengerId);
            await _hub.Clients.All.SendAsync("PassengerUpdated", updated);
        }
        return ok;
    }

    public async Task<bool> DeletePassengerAsync(Guid id)
    {
        var ok = await _inner.DeletePassengerAsync(id);
        if (ok)
        {
            await _hub.Clients.All.SendAsync("PassengerDeleted", id);
        }
        return ok;
    }
}