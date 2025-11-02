using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Microsoft.Data.SqlClient;

namespace Labs.Data.Repositories.Ado;

public class AdoTicketRepository : AdoBaseRepository, ITicketRepository
{
    public AdoTicketRepository(string connectionString) : base(connectionString)
    {
    }

    public async Task<IEnumerable<TicketInfoDto>> GetAllTicketsWithDetailsAsync()
    {
        var tickets = new List<TicketInfoDto>();

        var sql = @"
            SELECT 
                t.TicketId,
                p.LastName + ' ' + p.FirstName + ' ' + ISNULL(p.MiddleName, '') AS PassengerName,
                p.PhoneNumber,
                tr.TrainNumber,
                tt.TypeName AS TrainType,
                w.WagonNumber,
                wt.WagonTypeName AS WagonType,
                d.DestinationName,
                d.Distance,
                t.DepartureDateTime,
                t.ArrivalDateTime,
                d.BasePrice,
                wt.Surcharge AS WagonSurcharge,
                t.UrgencySurcharge,
                t.TotalPrice
            FROM Ticket t
            INNER JOIN Passenger p ON t.PassengerId = p.PassengerId
            INNER JOIN Train tr ON t.TrainId = tr.TrainId
            INNER JOIN TrainType tt ON tr.TrainTypeId = tt.TrainTypeId
            INNER JOIN Wagon w ON t.WagonId = w.WagonId
            INNER JOIN WagonType wt ON w.WagonTypeId = wt.WagonTypeId
            INNER JOIN Destination d ON t.DestinationId = d.DestinationId
            ORDER BY p.LastName, p.FirstName";

        using var connection = CreateConnection();
        using var command = new SqlCommand(sql, connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tickets.Add(new TicketInfoDto(
                reader.GetGuid(0),                              // TicketId
                reader.GetString(1).Trim(),                     // PassengerName
                reader.IsDBNull(2) ? "N/A" : reader.GetString(2), // PhoneNumber
                reader.GetString(3),                            // TrainNumber
                reader.GetString(4),                            // TrainType
                reader.GetString(5),                            // WagonNumber
                reader.GetString(6),                            // WagonType
                reader.GetString(7),                            // DestinationName
                reader.GetInt32(8),                             // Distance
                reader.GetDateTime(9),                          // DepartureDateTime
                reader.GetDateTime(10),                         // ArrivalDateTime
                reader.GetDecimal(11),                          // BasePrice
                reader.GetDecimal(12),                          // WagonSurcharge
                reader.GetDecimal(13),                          // UrgencySurcharge
                reader.GetDecimal(14)                           // TotalPrice
            ));
        }

        return tickets;
    }
}