using Labs.Application.DTOs.Response;

namespace Labs.MyMauiApp.Services;

public class TicketApiService
{
    private readonly ApiClient _apiClient;
    private const string BaseEndpoint = "api/tickets";

    public TicketApiService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<TicketInfoResponseDto>> GetAllTicketsAsync()
    {
        var tickets = await _apiClient.GetAsync<IEnumerable<TicketInfoResponseDto>>(BaseEndpoint);
        return tickets ?? [];
    }
}