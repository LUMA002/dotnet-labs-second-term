using System.Net.Http.Json;
using System.Text.Json;

namespace Labs.MyMauiApp.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        System.Diagnostics.Debug.WriteLine($"[ApiClient] Initialized with BaseAddress: {_httpClient.BaseAddress}");
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var url = $"{_httpClient.BaseAddress}{endpoint}";
            
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] GET {url}");
#endif
            
            var response = await _httpClient.GetAsync(endpoint);
            
#if DEBUG
            var content = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Response Status: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Response: {content.Substring(0, Math.Min(200, content.Length))}...");
#endif
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] HTTP Error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] BaseAddress: {_httpClient.BaseAddress}");
            throw new Exception($"Cannot connect to API at {_httpClient.BaseAddress}. Make sure WebAPI is running.", ex);
#else
            throw; 
#endif
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Error: {ex}");
            throw;
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        try
        {
            var url = $"{_httpClient.BaseAddress}{endpoint}";
            System.Diagnostics.Debug.WriteLine($"[ApiClient] POST {url}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Request Data: {JsonSerializer.Serialize(data, _jsonOptions)}");

            var response = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);

            var content = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Response Status: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] Response: {content}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] POST Error: {ex.Message}");          
            throw new Exception($"Cannot connect to API. Make sure WebAPI is running.", ex);
#endif        
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] POST Error: {ex}");
            throw;
        }
    }

    public async Task<bool> PutAsync<TRequest>(string endpoint, TRequest data)
    {
        try
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] PUT {endpoint}");
#endif
            var response = await _httpClient.PutAsJsonAsync(endpoint, data, _jsonOptions);
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] PUT Response: {response.StatusCode}");
#endif
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] PUT Error: {ex}");
#endif            
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpoint);
#if DEBUG            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] DELETE {endpoint}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] DELETE Response: {response.StatusCode}");
#endif          
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"[ApiClient] DELETE Error: {ex}");
#endif
            return false;
        }
    }
}