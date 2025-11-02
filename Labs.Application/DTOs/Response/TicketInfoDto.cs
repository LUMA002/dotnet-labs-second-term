namespace Labs.Application.DTOs.Response;

public record TicketInfoDto(
    Guid TicketId,
    string PassengerName,
    string PassengerPhone,
    string TrainNumber,
    string TrainType,
    string WagonNumber,
    string WagonType,
    string Destination,
    int Distance,
    DateTime DepartureDateTime,
    DateTime ArrivalDateTime,
    decimal BasePrice,
    decimal WagonSurcharge,
    decimal UrgencySurcharge,
    decimal TotalPrice
);