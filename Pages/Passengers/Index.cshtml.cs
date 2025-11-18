using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Passengers;

public class IndexModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(PassengerService passengerService, ILogger<IndexModel> logger)
    {
        _passengerService = passengerService;
        _logger = logger;
    }

    public IEnumerable<PassengerResponseDto> Passengers { get; set; } = [];

    [TempData]
    public string? SuccessMessage { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            Passengers = await _passengerService.GetAllPassengersAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading passengers");
            ErrorMessage = "Failed to load passengers.";
            Passengers = [];
        }
    }
}