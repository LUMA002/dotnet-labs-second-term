using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Labs.MVCApp.Controllers;

/// <summary>
/// Controller for viewing tickets (Read-only)
/// </summary>
public class TicketsController : Controller
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

    public async Task<IActionResult> Index()
    {
        try
        {
            var tickets = await _ticketService.GetAllTicketsWithDetailsAsync();
            return View(tickets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tickets");
            TempData["Error"] = "Error loading tickets";
            return View(new List<Labs.Application.DTOs.Response.TicketInfoResponseDto>());
        }
    }
}