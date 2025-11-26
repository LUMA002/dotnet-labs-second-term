using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labs.WebAPI.Controllers;

/// <summary>
/// Ticket management controller (Read-only)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(
        TicketService ticketService,
        ILogger<TicketsController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    /// <summary>
    /// Get all tickets with full details
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<TicketInfoResponseDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketInfoResponseDto>>> GetAll()
    {
        _logger.LogInformation("Getting all tickets");
        var tickets = await _ticketService.GetAllTicketsWithDetailsAsync();
        return Ok(tickets);
    }
}