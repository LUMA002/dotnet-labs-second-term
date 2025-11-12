using FluentValidation;
using Labs.Application.Interfaces;
using Labs.Application.Services;
using Labs.Application.Validators;
using Labs.Data;
using Labs.Data.Repositories;
using Labs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString) || connectionString == "PLACEHOLDER")
    throw new InvalidOperationException("Connection string not found. Configure User Secrets.");

// Add MVC services to the container.
builder.Services.AddControllersWithViews();

// FLUENTVALIDATION
builder.Services.AddValidatorsFromAssemblyContaining<CreatePassengerDtoValidator>();


builder.Services.AddDbContext<ReservationContext>(options =>
options.UseSqlServer(connectionString, sqlOptions =>
sqlOptions.MigrationsAssembly("Labs.Data")));

builder.Services.AddScoped<IRepository<Passenger>, Repository<Passenger>>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddScoped<PassengerService>();
builder.Services.AddScoped<TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Apply migrations on startup
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ReservationContext>();
//    await dbContext.Database.MigrateAsync();
//}
app.Run();
