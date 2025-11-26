using FluentValidation;
using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;
using Labs.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Labs.WebAPI.Controllers;

/// <summary>
/// Passenger management controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class PassengersController : ControllerBase
{
    private readonly IPassengerService _passengerService;
    private readonly IValidator<CreatePassengerRequestDto> _createValidator;
    private readonly IValidator<UpdatePassengerRequestDto> _updateValidator;
    private readonly ILogger<PassengersController> _logger;

    public PassengersController(
        IPassengerService passengerService,
        IValidator<CreatePassengerRequestDto> createValidator,
        IValidator<UpdatePassengerRequestDto> updateValidator,
        ILogger<PassengersController> logger)
    {
        _passengerService = passengerService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    /// <summary>
    /// Get all passengers
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<PassengerResponseDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PassengerResponseDto>>> GetAll()
    {
        _logger.LogInformation("Getting all passengers");
        var passengers = await _passengerService.GetAllPassengersAsync();
        return Ok(passengers);
    }

    /// <summary>
    /// Get passenger by ID
    /// </summary>
    /// <param name="id">Passenger ID</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<PassengerResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PassengerResponseDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting passenger with ID: {Id}", id);

        var passenger = await _passengerService.GetPassengerByIdAsync(id);

        if (passenger is null)
        {
            return NotFound(new { message = $"Passenger with ID {id} not found" });
        }

        return Ok(passenger);
    }

    /// <summary>
    /// Create new passenger
    /// </summary>
    /// <param name="request">Passenger data</param>
    [HttpPost]
    [ProducesResponseType<PassengerResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PassengerResponseDto>> Create(
        [FromBody] CreatePassengerRequestDto request)
    {
        _logger.LogInformation("Creating passenger: {LastName} {FirstName}",
            request.LastName, request.FirstName);

        var validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });

            return BadRequest(new { message = "Validation failed", errors });
        }

        var passenger = await _passengerService.CreatePassengerAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = passenger.Id },
            passenger);
    }

    /// <summary>
    /// Update existing passenger
    /// </summary>
    /// <param name="id">Passenger ID</param>
    /// <param name="request">Updated passenger data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdatePassengerRequestDto request)
    {
        if (id != request.PassengerId)
        {
            return BadRequest(new { message = "ID mismatch" });
        }

        _logger.LogInformation("Updating passenger with ID: {Id}", id);

        var validationResult = await _updateValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new { field = e.PropertyName, message = e.ErrorMessage });

            return BadRequest(new { message = "Validation failed", errors });
        }

        var success = await _passengerService.UpdatePassengerAsync(request);

        if (!success)
        {
            return NotFound(new { message = $"Passenger with ID {id} not found" });
        }

        return NoContent();
    }

    /// <summary>
    /// Delete passenger
    /// </summary>
    /// <param name="id">Passenger ID</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting passenger with ID: {Id}", id);

        var success = await _passengerService.DeletePassengerAsync(id);

        if (!success)
        {
            return NotFound(new { message = $"Passenger with ID {id} not found" });
        }

        return NoContent();
    }
}