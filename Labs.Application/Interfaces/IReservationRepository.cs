using Labs.Application.DTOs.Response;

namespace Labs.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationResponseDto>> GetAllForDisplayAsync();
    }
}