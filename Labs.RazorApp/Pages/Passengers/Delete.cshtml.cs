using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Passengers;

public class DeleteModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(PassengerService passengerService, ILogger<DeleteModel> logger)
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
            TempData["ErrorMessage"] = "Error loading passenger";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["ErrorMessage"] = "Invalid passenger ID";
            return RedirectToPage("./Index");
        }

        try
        {
            var success = await _passengerService.DeletePassengerAsync(id);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success
                ? "Passenger deleted successfully!"
                : "Passenger not found";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting passenger {Id}", id);
            TempData["ErrorMessage"] = "An error occurred while deleting the passenger";
            return RedirectToPage("./Index");
        }
    }
}