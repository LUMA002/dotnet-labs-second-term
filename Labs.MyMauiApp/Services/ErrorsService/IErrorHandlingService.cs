namespace Labs.MyMauiApp.Services;

/// <summary>
/// Error handling service for user-friendly error messages
/// </summary>
public interface IErrorHandlingService
{
    /// <summary>
    /// Handle exception and display user-friendly message
    /// </summary>
    Task HandleErrorAsync(Exception ex, string context = "");
}