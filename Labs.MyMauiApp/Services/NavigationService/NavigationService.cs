namespace Labs.MyMauiApp.Services;

/// <summary>
/// Implementation of navigation service using Shell
/// </summary>
public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task DisplayAlertAsync(string title, string message, string cancel)
    {
        await Shell.Current.DisplayAlertAsync(title, message, cancel);
    }

    public async Task<bool> DisplayConfirmAsync(string title, string message, string accept, string cancel)
    {
        return await Shell.Current.DisplayAlertAsync(title, message, accept, cancel);
    }
}