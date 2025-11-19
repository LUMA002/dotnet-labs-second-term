using FluentValidation;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.Application.Validators;
using Labs.Blazor.Components;
using Labs.Data;
using Labs.Data.Repositories;
using Labs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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


// DbContext
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Labs.Data")));

// Repositories & Services
builder.Services.AddScoped<IRepository<Passenger>, Repository<Passenger>>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

// Blazor Components
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
