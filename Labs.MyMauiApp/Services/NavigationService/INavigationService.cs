namespace Labs.MyMauiApp.Services;

/// <summary>
/// Navigation service for decoupling ViewModels from Shell navigation
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigate to a route
    /// </summary>
    Task NavigateToAsync(string route);

    /// <summary>
    /// Navigate back
    /// </summary>
    Task GoBackAsync();

    /// <summary>
    /// Display alert dialog
    /// </summary>
    Task DisplayAlertAsync(string title, string message, string cancel);

    /// <summary>
    /// Display confirmation dialog
    /// </summary>
    Task<bool> DisplayConfirmAsync(string title, string message, string accept, string cancel);
}