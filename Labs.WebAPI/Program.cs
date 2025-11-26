using FluentValidation;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.Application.Validators;
using Labs.Data;
using Labs.Data.Repositories;
using Labs.Domain.Entities;
using Labs.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString) || connectionString == "PLACEHOLDER")
    throw new InvalidOperationException("Connection string not found. Configure User Secrets.");

// Add services to the container
builder.Services.AddControllers();

// Handle cycles 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy =
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new()
    {
        Title = "Ticket Reservation API",
        Version = "v1",
        Description = "Web API for managing passenger tickets and reservations"
    });
});

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePassengerDtoValidator>();

// DbContext
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Labs.Data")));

// Repositories
builder.Services.AddScoped<IRepository<Passenger>, Repository<Passenger>>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Services
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<TicketService>();

// CORS (for frontend)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

var app = builder.Build();

// Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Reservation API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

//app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();