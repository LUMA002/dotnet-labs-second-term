using Labs.Application.Interfaces;
using Labs.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Labs.Data.Repositories.Ado;

public class AdoPassengerRepository : AdoBaseRepository, IRepository<Passenger>
{
    public AdoPassengerRepository(string connectionString) : base(connectionString)
    {
    }

    public async Task<IEnumerable<Passenger>> GetAllAsync()
    {
        var passengers = new List<Passenger>();

        var sql = @"
            SELECT PassengerId, FirstName, LastName, MiddleName, Address, PhoneNumber
            FROM Passenger
            ORDER BY LastName, FirstName";

        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        // Execute query and read results
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            passengers.Add(new Passenger
            {
                PassengerId = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3),
                Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                PhoneNumber = reader.IsDBNull(5) ? null : reader.GetString(5)
            });
        }

        return passengers;
    }

    public async Task<Passenger?> GetByIdAsync(Guid id)
    {
        var sql = @"
            SELECT PassengerId, FirstName, LastName, MiddleName, Address, PhoneNumber
            FROM Passenger
            WHERE PassengerId = @PassengerId";

        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@PassengerId", id);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Passenger
            {
                PassengerId = reader.GetGuid(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3),
                Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                PhoneNumber = reader.IsDBNull(5) ? null : reader.GetString(5)
            };
        }

        return null;
    }


    public async Task<Passenger> AddAsync(Passenger entity)
    {
        var sql = @"
            INSERT INTO Passenger (PassengerId, FirstName, LastName, MiddleName, Address, PhoneNumber)
            VALUES (@PassengerId, @FirstName, @LastName, @MiddleName, @Address, @PhoneNumber)";

        var parameters = new[]
        {
            new SqlParameter("@PassengerId", entity.PassengerId),
            new SqlParameter("@FirstName", entity.FirstName),
            new SqlParameter("@LastName", entity.LastName),
            new SqlParameter("@MiddleName", (object?)entity.MiddleName ?? DBNull.Value),
            new SqlParameter("@Address", (object?)entity.Address ?? DBNull.Value),
            new SqlParameter("@PhoneNumber", (object?)entity.PhoneNumber ?? DBNull.Value)
        };

        await ExecuteNonQueryAsync(sql, parameters);

        return entity;
    }


    public async Task UpdateAsync(Passenger entity)
    {
        var sql = @"
            UPDATE Passenger
            SET FirstName = @FirstName,
                LastName = @LastName,
                MiddleName = @MiddleName,
                Address = @Address,
                PhoneNumber = @PhoneNumber
            WHERE PassengerId = @PassengerId";

        var parameters = new[]
        {
            new SqlParameter("@PassengerId", entity.PassengerId),
            new SqlParameter("@FirstName", entity.FirstName),
            new SqlParameter("@LastName", entity.LastName),
            new SqlParameter("@MiddleName", (object?)entity.MiddleName ?? DBNull.Value),
            new SqlParameter("@Address", (object?)entity.Address ?? DBNull.Value),
            new SqlParameter("@PhoneNumber", (object?)entity.PhoneNumber ?? DBNull.Value)
        };

        await ExecuteNonQueryAsync(sql, parameters);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Passenger WHERE PassengerId = @PassengerId";

        await ExecuteNonQueryAsync(sql, new SqlParameter("@PassengerId", id));
    }

    public async Task<int> SaveChangesAsync()
    {
        // ADO.NET execute commands instantly, so SaveChanges isn't necessary
        return await Task.FromResult(0); // plug
    }
}