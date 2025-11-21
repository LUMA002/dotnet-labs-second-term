using FluentValidation;
using Labs.Application.DTOs.Request;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Passengers;

public class EditModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly IValidator<UpdatePassengerRequestDto> _validator;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        PassengerService passengerService,
        IValidator<UpdatePassengerRequestDto> validator,
        ILogger<EditModel> logger)
    {
        _passengerService = passengerService;
        _validator = validator;
        _logger = logger;
    }

    [BindProperty]
    public Guid PassengerId { get; set; } // better to do it private

    [BindProperty]
    public string? FirstName { get; set; }

    [BindProperty]
    public string? LastName { get; set; }

    [BindProperty]
    public string? MiddleName { get; set; }

    [BindProperty]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    public string? Address { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["ErrorMessage"] = "Invalid passenger ID"; // bad practice to use TempData
            return RedirectToPage("./Index");
        }

        try
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);

            if (passenger == null)
            {
                TempData["ErrorMessage"] = "Passenger not found";
                return RedirectToPage("./Index");
            }

            PassengerId = passenger.Id;
            FirstName = passenger.FirstName;
            LastName = passenger.LastName;
            MiddleName = passenger.MiddleName;
            PhoneNumber = passenger.PhoneNumber;
            Address = passenger.Address;

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading passenger {Id}", id);
            TempData["ErrorMessage"] = "Error loading passenger";
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = new UpdatePassengerRequestDto(
            PassengerId,
            FirstName ?? string.Empty,
            LastName ?? string.Empty,
            MiddleName,
            Address,
            PhoneNumber
        );

        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return Page();
        }

        try
        {
            var success = await _passengerService.UpdatePassengerAsync(dto);

            if (!success)
            {
                TempData["ErrorMessage"] = "Passenger not found";
                return RedirectToPage("./Index");
            }

            TempData["SuccessMessage"] = "Passenger updated successfully!";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating passenger {Id}", PassengerId);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the passenger.");
            return Page();
        }
    }
}