using FluentValidation;
using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;
using Labs.Application.Services;
using Labs.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Labs.MVCApp.Controllers;

/// <summary>
/// Controller for managing passengers (CRUD operations)
/// Uses PassengerService from Application Layer
/// </summary>
public class PassengersController : Controller
{
    private readonly PassengerService _passengerService;
    private readonly ILogger<PassengersController> _logger;
    private readonly IValidator<CreatePassengerRequestDto> _createValidator;
    private readonly IValidator<UpdatePassengerRequestDto> _updateValidator;

    public PassengersController(
        PassengerService passengerService,
        ILogger<PassengersController> logger,
        IValidator<CreatePassengerRequestDto> createValidator,
        IValidator<UpdatePassengerRequestDto> updateValidator)
    {
        _passengerService = passengerService;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            return View(passengers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving passengers");
            TempData["Error"] = "Error loading passengers";
            return View(Enumerable.Empty<PassengerResponseDto>());
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["Error"] = "Invalid passenger ID";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);
            
            if (passenger == null)
            {
                TempData["Error"] = "Passenger not found";
                return RedirectToAction(nameof(Index));
            }

            return View(passenger);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving passenger {Id}", id);
            TempData["Error"] = "Error loading passenger details";
            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePassengerRequestDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(dto);
        }

        try
        {
            await _passengerService.CreatePassengerAsync(dto);
            TempData["Success"] = "Passenger created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (CustomValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating passenger");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the passenger");
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["Error"] = "Invalid passenger ID";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);
            
            if (passenger == null)
            {
                TempData["Error"] = "Passenger not found";
                return RedirectToAction(nameof(Index));
            }

            // Convert PassengerResponseDto to UpdatePassengerRequestDto
            var dto = new UpdatePassengerRequestDto(
                passenger.Id,
                passenger.FirstName,
                passenger.LastName,
                passenger.MiddleName,
                passenger.Address,
                passenger.PhoneNumber
            );

            return View(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading passenger for edit {Id}", id);
            TempData["Error"] = "Error loading passenger";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdatePassengerRequestDto dto)
    {
        if (id != dto.PassengerId)
        {
            return BadRequest("ID mismatch");
        }

        var validationResult = await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View(dto);
        }

        try
        {
            var success = await _passengerService.UpdatePassengerAsync(dto);
            
            if (!success)
            {
                TempData["Error"] = "Passenger not found";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Passenger updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (CustomValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating passenger {Id}", id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the passenger");
            return View(dto);
        }
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["Error"] = "Invalid passenger ID";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);
            
            if (passenger == null)
            {
                TempData["Error"] = "Passenger not found";
                return RedirectToAction(nameof(Index));
            }

            return View(passenger);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading passenger for delete {Id}", id);
            TempData["Error"] = "Error loading passenger";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (id == Guid.Empty)
        {
            TempData["Error"] = "Invalid passenger ID";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var success = await _passengerService.DeletePassengerAsync(id);
            
            TempData[success ? "Success" : "Error"] = success 
                ? "Passenger deleted successfully" 
                : "Passenger not found";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting passenger {Id}", id);
            TempData["Error"] = "An error occurred while deleting the passenger";
            return RedirectToAction(nameof(Index));
        }
    }
}