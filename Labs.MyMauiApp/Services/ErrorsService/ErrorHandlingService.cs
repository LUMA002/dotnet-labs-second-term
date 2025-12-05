using System.Net;

namespace Labs.MyMauiApp.Services;

/// <summary>
/// Implementation of error handling service
/// </summary>
public class ErrorHandlingService : IErrorHandlingService
{
    private readonly INavigationService _navigationService;

    public ErrorHandlingService(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public async Task HandleErrorAsync(Exception ex, string context = "")
    {
        var (title, message) = MapException(ex, context);
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"[ERROR] {context}: {ex}");
#endif
        await _navigationService.DisplayAlertAsync(title, message, "OK");
    }

    /// <summary>
    /// Map exceptions to user-friendly messages
    /// </summary>
    private (string Title, string Message) MapException(Exception ex, string context)
    {
        return ex switch
        {
            HttpRequestException httpEx when httpEx.StatusCode == HttpStatusCode.NotFound
                => ("Not Found", $"{context} not found. It may have been deleted."),

            HttpRequestException httpEx when httpEx.StatusCode == HttpStatusCode.BadRequest
                => ("Invalid Data", "Please check your input and try again."),

            TaskCanceledException
                => ("Timeout", "The request took too long. Please try again."),

            _ => ("Error", "An unexpected error occurred. Please try again later.")
        };
    }
}