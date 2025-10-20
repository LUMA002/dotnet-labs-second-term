using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;

namespace Labs.Application.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        // cервіс залежить від інтерфейсу, а не від конкретної реалізації (DI)
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Task<IEnumerable<ReservationResponseDto>> GetAllReservationsAsync()
        {
            return _reservationRepository.GetAllForDisplayAsync();
        }
    }
}