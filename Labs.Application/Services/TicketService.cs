using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;

namespace Labs.Application.Services;

/// <summary>
/// Service for ticket operations
/// Clean Arch: depends only on ITicketRepository interface, not on DbContext
/// </summary>
public class TicketService 
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<TicketInfoDto>> GetAllTicketsWithDetailsAsync()
    {
        return await _ticketRepository.GetAllTicketsWithDetailsAsync();
    }
}