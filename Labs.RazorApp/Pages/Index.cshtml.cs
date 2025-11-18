using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages;

public class IndexModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly TicketService _ticketService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        PassengerService passengerService,
        TicketService ticketService,
        ILogger<IndexModel> logger)
    {
        _passengerService = passengerService;
        _ticketService = ticketService;
        _logger = logger;
    }

    // props for View
    public int TotalPassengers { get; set; }
    public int TotalTickets { get; set; }
    public decimal TotalRevenue { get; set; }
    public IEnumerable<TicketInfoResponseDto> RecentTickets { get; set; } = [];

    // handler for GET request
    public async Task OnGetAsync()
    {
        try
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            var tickets = await _ticketService.GetAllTicketsWithDetailsAsync();

            TotalPassengers = passengers.Count();
            TotalTickets = tickets.Count();
            TotalRevenue = tickets.Sum(t => t.TotalPrice);
            RecentTickets = tickets.OrderByDescending(t => t.DepartureDateTime).Take(5);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard data");
            TotalPassengers = 0;
            TotalTickets = 0;
            TotalRevenue = 0;
            RecentTickets = [];
        }
    }
}