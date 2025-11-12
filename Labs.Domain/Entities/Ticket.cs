namespace Labs.Domain.Entities;

public sealed class Ticket
{
    public Guid TicketId { get; init; } // change to Id

    public Guid PassengerId { get; set; }
    public Passenger Passenger { get; set; } = null!;
    public Guid TrainId { get; set; }
    public Train Train { get; set; } = null!;
    public Guid WagonId { get; set; }  
    public Wagon Wagon { get; set; } = null!;
    public Guid DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;

    public DateTime DepartureDateTime { get; set; } // change to DateTimeOffset
    public DateTime ArrivalDateTime { get; set; } // change to DateTimeOffset
    public decimal UrgencySurcharge { get; set; }
    public decimal TotalPrice { get; set; } 
}