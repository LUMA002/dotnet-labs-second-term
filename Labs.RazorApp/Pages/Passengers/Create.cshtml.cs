using FluentValidation;
using Labs.Application.DTOs.Request;
using Labs.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labs.RazorApp.Pages.Passengers;

public class CreateModel : PageModel
{
    private readonly PassengerService _passengerService;
    private readonly IValidator<CreatePassengerRequestDto> _validator;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        PassengerService passengerService,
        IValidator<CreatePassengerRequestDto> validator,
        ILogger<CreateModel> logger)
    {
        _passengerService = passengerService;
        _validator = validator;
        _logger = logger;
    }

    [BindProperty]
    public string? FirstName { get; set; }

    [BindProperty]
    public string? LastName { get; set; }

    [BindProperty]
    public string? MiddleName { get; set; }

    [BindProperty]
    public string? Address { get; set; }

    [BindProperty]
    public string? PhoneNumber { get; set; }

    public void OnGet()
    {
        // initialiation form
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreatePassengerRequestDto(
            FirstName,
            LastName,
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
            await _passengerService.CreatePassengerAsync(dto);
            TempData["SuccessMessage"] = "Passenger created successfully!";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating passenger");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the passenger.");
            return Page();
        }
    }
}