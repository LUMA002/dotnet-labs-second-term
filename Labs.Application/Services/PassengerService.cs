using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Labs.Domain.Entities;
using Labs.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Labs.Application.Services;

public class PassengerService
{
    private readonly IRepository<Passenger> _repository;

    public PassengerService(IRepository<Passenger> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PassengerDto>> GetAllPassengersAsync()
    {
        var passengers = await _repository.GetAllAsync();
        return passengers.Select(p => new PassengerDto(
            p.PassengerId,
            p.FirstName,
            p.LastName,
            p.MiddleName,
            p.Address,
            p.PhoneNumber
        ));
    }

    public async Task<PassengerDto?> GetPassengerByIdAsync(Guid id)
    {
        var passenger = await _repository.GetByIdAsync(id);
        return passenger == null ? null : new PassengerDto(
            passenger.PassengerId,
            passenger.FirstName,
            passenger.LastName,
            passenger.MiddleName,
            passenger.Address,
            passenger.PhoneNumber
        );
    }

    public async Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto dto)
    {
        ValidatePassenger(dto.FirstName, dto.LastName, dto.MiddleName, dto.Address, dto.PhoneNumber);

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

        return new PassengerDto(
            passenger.PassengerId,
            passenger.FirstName,
            passenger.LastName,
            passenger.MiddleName,
            passenger.Address,
            passenger.PhoneNumber
        );
    }

    public async Task<bool> UpdatePassengerAsync(Guid id, CreatePassengerDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return false;

        ValidatePassenger(dto.FirstName, dto.LastName, dto.MiddleName, dto.Address, dto.PhoneNumber);

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

    private static void ValidatePassenger(
        string firstName,
        string lastName,
        string? middleName,
        string? address,
        string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 100)
            throw new ValidationException("First name must be between 2 and 100 characters");

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 100)
            throw new ValidationException("Last name must be between 2 and 100 characters");

        if (!string.IsNullOrWhiteSpace(middleName) && (middleName.Length < 2 || middleName.Length > 100))
            throw new ValidationException("Middle name must be between 2 and 100 characters");

        if (!string.IsNullOrWhiteSpace(address) && address.Length > 255)
            throw new ValidationException("Address is too long (max 255 characters)");

        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            var phoneRegex = new Regex(@"^\+?[0-9]{10,17}$");
            if (!phoneRegex.IsMatch(phoneNumber))
                throw new ValidationException("Invalid phone format. Expected: +380XXXXXXXXX or XXXXXXXXX");
        }
    }
}