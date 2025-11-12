namespace Labs.Application.DTOs.Response;

/// <summary>
/// DTO for displaying ticket information with all related data
/// </summary>
public record TicketInfoResponseDto(
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