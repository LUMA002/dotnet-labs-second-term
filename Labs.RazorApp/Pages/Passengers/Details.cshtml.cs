using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Passengers;

public class DetailsModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(PassengerService passengerService, ILogger<DetailsModel> logger)
    {
        _passengerService = passengerService;
        _logger = logger;
    }

    public PassengerResponseDto? Passenger { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["ErrorMessage"] = "Invalid passenger ID";
            return RedirectToPage("./Index");
        }

        try
        {
            Passenger = await _passengerService.GetPassengerByIdAsync(id);

            if (Passenger == null)
            {
                TempData["ErrorMessage"] = "Passenger not found";
                return RedirectToPage("./Index");
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading passenger {Id}", id);
            TempData["ErrorMessage"] = "Error loading passenger details";
            return RedirectToPage("./Index");
        }
    }
}