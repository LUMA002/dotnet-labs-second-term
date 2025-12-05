using Labs.Application.DTOs.Request;
using Labs.Application.DTOs.Response;

namespace Labs.MyMauiApp.Services;

public class PassengerApiService
{
    private readonly ApiClient _apiClient;
    private const string BaseEndpoint = "api/passengers";

    public PassengerApiService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<PassengerResponseDto>> GetAllAsync()
    {
        var passengers = await _apiClient.GetAsync<IEnumerable<PassengerResponseDto>>(BaseEndpoint);
        return passengers ?? [];
    }

    public async Task<PassengerResponseDto?> GetByIdAsync(Guid id)
    {
        return await _apiClient.GetAsync<PassengerResponseDto>($"{BaseEndpoint}/{id}");
    }

    public async Task<PassengerResponseDto?> CreateAsync(CreatePassengerRequestDto dto)
    {
        return await _apiClient.PostAsync<CreatePassengerRequestDto, PassengerResponseDto>(BaseEndpoint, dto);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePassengerRequestDto dto)
    {
        return await _apiClient.PutAsync($"{BaseEndpoint}/{id}", dto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _apiClient.DeleteAsync($"{BaseEndpoint}/{id}");
    }
}