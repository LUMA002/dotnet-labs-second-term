using Labs.Application.DTOs.Request;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.Data.Repositories.Ado;
using Labs.Domain.Entities;
using Labs.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false);
        config.AddUserSecrets<Program>(optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString) || connectionString == "PLACEHOLDER")
            throw new InvalidOperationException("Connection string not found");

        // ADO.NET Repositories
        services.AddScoped<IRepository<Passenger>>(sp =>
            new AdoPassengerRepository(connectionString));
        services.AddScoped<ITicketRepository>(sp =>
            new AdoTicketRepository(connectionString));

        // Services
        services.AddScoped<PassengerService>();
        services.AddScoped<TicketService>();
    })
    .Build();

Console.WriteLine("Ticket Reservation System (ADO.NET)\n");

using var scope = host.Services.CreateScope();
var passengerService = scope.ServiceProvider.GetRequiredService<PassengerService>();
var ticketService = scope.ServiceProvider.GetRequiredService<TicketService>();

bool exit = false;

while (!exit)
{
    Console.WriteLine("\nMAIN MENU");
    Console.WriteLine("1. View all passengers");
    Console.WriteLine("2. Add new passenger");
    Console.WriteLine("3. Update passenger");
    Console.WriteLine("4. Delete passenger");
    Console.WriteLine("5. Find passenger by ID");
    Console.WriteLine("6. View all tickets");
    Console.WriteLine("0. Exit");
    Console.Write("\nSelect option: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1": await ShowAllPassengers(passengerService); break;
            case "2": await AddNewPassenger(passengerService); break;
            case "3": await UpdatePassenger(passengerService); break;
            case "4": await DeletePassenger(passengerService); break;
            case "5": await FindPassengerById(passengerService); break;
            case "6": await ShowAllTickets(ticketService); break;
            case "0":
                exit = true;
                Console.WriteLine("\nGoodbye!");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"\nValidation error: {ex.Message}");
    }
    catch (ArgumentException ex) // for another cases
    {
        Console.WriteLine($"\nValidation error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nError: {ex.Message}");
    }
}

async Task ShowAllPassengers(PassengerService service)
{
    Console.WriteLine("\nPASSENGERS LIST");
    Console.WriteLine(new string('-', 60));

    var passengers = await service.GetAllPassengersAsync();

    if (!passengers.Any())
    {
        Console.WriteLine("No passengers found");
        return;
    }

    foreach (var p in passengers)
    {
        Console.WriteLine($"\nID: {p.Id}");
        Console.WriteLine($"Name: {p.LastName} {p.FirstName} {p.MiddleName}");
        Console.WriteLine($"Phone: {p.PhoneNumber ?? "not specified"}");
        Console.WriteLine($"Address: {p.Address ?? "not specified"}");
    }
}

async Task AddNewPassenger(PassengerService service)
{
    Console.WriteLine("\nADD NEW PASSENGER");
    Console.WriteLine(new string('-', 60));

    Console.Write("First name: ");
    var firstName = Console.ReadLine() ?? "";

    Console.Write("Last name: ");
    var lastName = Console.ReadLine() ?? "";

    Console.Write("Middle name (optional): ");
    var middleName = Console.ReadLine();

    Console.Write("Address (optional): ");
    var address = Console.ReadLine();

    Console.Write("Phone (+380XXXXXXXXX or skip): ");
    var phone = Console.ReadLine();

    var dto = new CreatePassengerDto(
        firstName,
        lastName,
        string.IsNullOrWhiteSpace(middleName) ? null : middleName,
        string.IsNullOrWhiteSpace(address) ? null : address,
        string.IsNullOrWhiteSpace(phone) ? null : phone
    );

    var passenger = await service.CreatePassengerAsync(dto);
    Console.WriteLine($"\nPassenger created successfully! ID: {passenger.Id}");
}

async Task UpdatePassenger(PassengerService service)
{
    Console.WriteLine("\nUPDATE PASSENGER");
    Console.WriteLine(new string('-', 60));

    Console.Write("Enter passenger ID: ");
    if (!Guid.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID format");
        return;
    }

    var passenger = await service.GetPassengerByIdAsync(id);
    if (passenger == null)
    {
        Console.WriteLine("Passenger not found");
        return;
    }

    Console.WriteLine($"\nCurrent: {passenger.LastName} {passenger.FirstName}");
    Console.WriteLine("Enter new data (press Enter to keep current):\n");

    Console.Write($"First name [{passenger.FirstName}]: ");
    var firstName = Console.ReadLine();
    firstName = string.IsNullOrWhiteSpace(firstName) ? passenger.FirstName : firstName;

    Console.Write($"Last name [{passenger.LastName}]: ");
    var lastName = Console.ReadLine();
    lastName = string.IsNullOrWhiteSpace(lastName) ? passenger.LastName : lastName;

    Console.Write($"Middle name [{passenger.MiddleName}]: ");
    var middleName = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(middleName)) middleName = passenger.MiddleName;

    Console.Write($"Phone [{passenger.PhoneNumber}]: ");
    var phone = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(phone)) phone = passenger.PhoneNumber;

    Console.Write($"Address [{passenger.Address}]: ");
    var address = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(address)) address = passenger.Address;

    var dto = new CreatePassengerDto(firstName, lastName, middleName, address, phone);
    var success = await service.UpdatePassengerAsync(id, dto);

    Console.WriteLine(success ? "\nUpdated successfully" : "\nUpdate failed");
}

async Task DeletePassenger(PassengerService service)
{
    Console.WriteLine("\nDELETE PASSENGER");
    Console.WriteLine(new string('-', 60));

    Console.Write("Enter passenger ID: ");
    if (!Guid.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID format");
        return;
    }

    var passenger = await service.GetPassengerByIdAsync(id);
    if (passenger == null)
    {
        Console.WriteLine("Passenger not found");
        return;
    }

    Console.WriteLine($"\nAre you sure you want to delete: {passenger.LastName} {passenger.FirstName}?");
    Console.Write("Type 'yes' to confirm: ");

    if (Console.ReadLine()?.ToLower() == "yes")
    {
        var success = await service.DeletePassengerAsync(id);
        Console.WriteLine(success ? "\nDeleted successfully" : "\nDeletion failed");
    }
    else
    {
        Console.WriteLine("\nCancelled");
    }
}

async Task FindPassengerById(PassengerService service)
{
    Console.WriteLine("\nFIND PASSENGER");
    Console.WriteLine(new string('-', 60));

    Console.Write("Enter passenger ID: ");
    if (!Guid.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Invalid ID format");
        return;
    }

    var passenger = await service.GetPassengerByIdAsync(id);

    if (passenger == null)
    {
        Console.WriteLine("Passenger not found");
        return;
    }

    Console.WriteLine($"\nID: {passenger.Id}");
    Console.WriteLine($"Name: {passenger.LastName} {passenger.FirstName} {passenger.MiddleName}");
    Console.WriteLine($"Phone: {passenger.PhoneNumber ?? "not specified"}");
    Console.WriteLine($"Address: {passenger.Address ?? "not specified"}");
}

async Task ShowAllTickets(TicketService service)
{
    Console.WriteLine("\nALL TICKETS");
    Console.WriteLine(new string('=', 90));

    var tickets = await service .GetAllTicketsWithDetailsAsync();

    if (!tickets.Any())
    {
        Console.WriteLine("No tickets found");
        return;
    }

    foreach (var t in tickets)
    {
        Console.WriteLine($"\nTicket ID: {t.TicketId}");
        Console.WriteLine($"Passenger: {t.PassengerName} | Phone: {t.PassengerPhone}");
        Console.WriteLine($"Train: {t.TrainNumber} ({t.TrainType}) | Wagon: {t.WagonNumber} ({t.WagonType})");
        Console.WriteLine($"Destination: {t.Destination} ({t.Distance} km)");
        Console.WriteLine($"Departure: {t.DepartureDateTime:yyyy-MM-dd HH:mm} | Arrival: {t.ArrivalDateTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Price: Base {t.BasePrice:C} + Wagon {t.WagonSurcharge:C} + Urgency {t.UrgencySurcharge:C} = TOTAL {t.TotalPrice:C}");
        Console.WriteLine(new string('-', 90));
    }
}