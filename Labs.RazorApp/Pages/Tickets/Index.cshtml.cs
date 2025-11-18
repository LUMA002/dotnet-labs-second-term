using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Tickets;

public class IndexModel : PageModel
{
    private readonly TicketService _ticketService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(TicketService ticketService, ILogger<IndexModel> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    public IEnumerable<TicketInfoResponseDto> Tickets { get; set; } = [];

    [TempData]
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            Tickets = await _ticketService.GetAllTicketsWithDetailsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading tickets");
            ErrorMessage = "Failed to load tickets.";
            Tickets = [];
        }
    }
}