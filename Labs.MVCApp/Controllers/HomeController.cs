using Labs.Application.Services;
using Labs.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Labs.MVCApp.Controllers;

/// <summary>
/// Home controller - main page of the application with statistics dashboard
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PassengerService _passengerService;
    private readonly TicketService _ticketService;

    public HomeController(
        ILogger<HomeController> logger,
        PassengerService passengerService,
        TicketService ticketService)
    {
        _logger = logger;
        _passengerService = passengerService;
        _ticketService = ticketService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            var tickets = await _ticketService.GetAllTicketsWithDetailsAsync();

            // pass data to view using ViewBag not ViewModel for simplicity
            ViewBag.TotalPassengers = passengers.Count();
            ViewBag.TotalTickets = tickets.Count();
            ViewBag.TotalRevenue = tickets.Sum(t => t.TotalPrice);
            ViewBag.RecentTickets = tickets.OrderByDescending(t => t.DepartureDateTime).Take(5);

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading dashboard");
            // if error - show empty dashboard
            ViewBag.TotalPassengers = 0;
            ViewBag.TotalTickets = 0;
            ViewBag.TotalRevenue = 0m;
            ViewBag.RecentTickets = new List<Labs.Application.DTOs.Response.TicketInfoResponseDto>();
            return View();
        }
    }

    /// <summary>
    /// Error page (global, pinned in main cs file of MVC) - shown when exception occurs
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}