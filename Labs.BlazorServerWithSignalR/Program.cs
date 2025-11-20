using FluentValidation;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.Application.Validators;
using Labs.BlazorServerWithSignalR.Hubs;
using Labs.BlazorServerWithSignalR.Services;
using Labs.BlazorServerWithSignalR.Components;
using Labs.Data;
using Labs.Data.Repositories;
using Labs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Connection String
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString) || connectionString == "PLACEHOLDER")
    throw new InvalidOperationException("Connection string not found.");

// Blazor Services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePassengerDtoValidator>();

// SignalR
builder.Services.AddSignalR();

// DbContext
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Labs.Data")));

// Repositories & Services
builder.Services.AddScoped<IRepository<Passenger>, Repository<Passenger>>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Register the concrete inner service
builder.Services.AddScoped<PassengerService>();

// Expose IPassengerService via SignalR-decorated wrapper
builder.Services.AddScoped<IPassengerService>(sp =>
    new SignalRPassengerServiceDecorator(
        sp.GetRequiredService<PassengerService>(),
        sp.GetRequiredService<IHubContext<PassengerHub>>()
    ));

builder.Services.AddScoped<TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// Blazor Components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map custom hub (does not affect Blazor internal /_blazor)
app.MapHub<PassengerHub>("/hubs/passengers");

await app.RunAsync();
