using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Labs.Domain.Entities;

namespace Labs.Application.Services;

public class PassengerService
{
    private readonly IRepository<Passenger> _repository;

    public PassengerService(IRepository<Passenger> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PassengerResponseDto>> GetAllPassengersAsync()
    {
        var passengers = await _repository.GetAllAsync();
        return passengers.Select(p => new PassengerResponseDto(
            p.PassengerId,
            p.FirstName,
            p.LastName,
            p.MiddleName,
            p.Address,
            p.PhoneNumber
        ));
    }

    public async Task<PassengerResponseDto?> GetPassengerByIdAsync(Guid id)
    {
        var passenger = await _repository.GetByIdAsync(id);
        return passenger == null ? null : new PassengerResponseDto(
            passenger.PassengerId,
            passenger.FirstName,
            passenger.LastName,
            passenger.MiddleName,
            passenger.Address,
            passenger.PhoneNumber
        );
    }

    public async Task<PassengerResponseDto> CreatePassengerAsync(CreatePassengerRequestDto dto)
    {
        var passenger = new Passenger
        {
            PassengerId = Guid.NewGuid(),
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            MiddleName = dto.MiddleName?.Trim(),
            Address = dto.Address?.Trim(),
            PhoneNumber = dto.PhoneNumber?.Trim()
        };

        await _repository.AddAsync(passenger);
        await _repository.SaveChangesAsync();

        return new PassengerResponseDto(
            passenger.PassengerId,
            passenger.FirstName,
            passenger.LastName,
            passenger.MiddleName,
            passenger.Address,
            passenger.PhoneNumber
        );
    }
    public async Task<bool> UpdatePassengerAsync(UpdatePassengerRequestDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.PassengerId);
        if (existing == null) return false;

        existing.FirstName = dto.FirstName.Trim();
        existing.LastName = dto.LastName.Trim();
        existing.MiddleName = dto.MiddleName?.Trim();
        existing.Address = dto.Address?.Trim();
        existing.PhoneNumber = dto.PhoneNumber?.Trim();

        await _repository.UpdateAsync(existing);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePassengerAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return false;

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return true;
    }
}