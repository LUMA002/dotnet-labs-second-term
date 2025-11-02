using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labs.Data.Repositories;

/// <summary>
/// EF Core implementation of ITicketRepository
/// </summary>
public class TicketRepository : ITicketRepository
{
    private readonly ReservationContext _context;

    public TicketRepository(ReservationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketInfoDto>> GetAllTicketsWithDetailsAsync()
    {
        return await _context.Tickets
            .Include(t => t.Passenger   )
            .Include(t => t.Train)
                .ThenInclude(tr => tr!.TrainType)
            .Include(t => t.Wagon)
                .ThenInclude(w => w!.WagonType)
            .Include(t => t.Destination)
            .OrderBy(t => t.Passenger.LastName) 
            .ThenBy(t => t.Passenger.FirstName)
            .Select(t => new TicketInfoDto(
                t.TicketId,
                $"{t.Passenger.LastName} {t.Passenger.FirstName} {t.Passenger.MiddleName ?? ""}".Trim(),
                t.Passenger.PhoneNumber ?? "N/A",
                t.Train.TrainNumber,
                t.Train.TrainType!.TypeName,
                t.Wagon.WagonNumber,
                t.Wagon.WagonType!.WagonTypeName,
                t.Destination.DestinationName,
                t.Destination.Distance,
                t.DepartureDateTime,
                t.ArrivalDateTime,
                t.Destination.BasePrice,
                t.Wagon.WagonType.Surcharge,
                t.UrgencySurcharge,
                t.TotalPrice
            ))
            .ToListAsync();
    }
}