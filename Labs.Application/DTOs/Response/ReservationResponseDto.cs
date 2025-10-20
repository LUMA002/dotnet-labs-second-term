namespace Labs.Application.DTOs.Response
{
    public record ReservationResponseDto(
        string PassengerFullName,
        string TrainNumber,
        string DestinationName,
        string TrainType,
        string WagonType,
        decimal TotalPrice
    );
}