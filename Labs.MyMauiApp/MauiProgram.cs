using Microsoft.Extensions.Logging;
using Labs.MyMauiApp.Services;
using Labs.MyMauiApp.ViewModels;
using Labs.MyMauiApp.Pages.Tickets;
using Labs.MyMauiApp.Pages.Passengers;

namespace Labs.MyMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // HTTP CLIENT CONFIGURATION
        builder.Services.AddHttpClient<ApiClient>(client =>
        {
#if ANDROID
            // Android emulator localhost 
            client.BaseAddress = new Uri("http://10.0.2.2:5155/");
#else
            client.BaseAddress = new Uri("http://localhost:5155/");
#endif
            client.Timeout = TimeSpan.FromSeconds(15);
        });

        // SERVICES
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IErrorHandlingService, ErrorHandlingService>();
        builder.Services.AddScoped<TicketApiService>();
        builder.Services.AddScoped<PassengerApiService>();

        // VIEWMODELS
        builder.Services.AddTransient<TicketsViewModel>();
        builder.Services.AddTransient<PassengersViewModel>();
        builder.Services.AddTransient<CreatePassengerViewModel>();
        builder.Services.AddTransient<EditPassengerViewModel>();

        // PAGES
        builder.Services.AddTransient<TicketsPage>();
        builder.Services.AddTransient<PassengersPage>();
        builder.Services.AddTransient<CreatePassengerPage>();
        builder.Services.AddTransient<EditPassengerPage>();

        return builder.Build();
    }
}