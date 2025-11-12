using Labs.Application.DTOs.Response;

namespace Labs.Application.Interfaces;

/// <summary>
/// Repository interface for Ticket operations
/// </summary>
public interface ITicketRepository
{
    Task<IEnumerable<TicketInfoResponseDto>> GetAllTicketsWithDetailsAsync();
}