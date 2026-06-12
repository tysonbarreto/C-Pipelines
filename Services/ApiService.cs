

namespace AlbumPipeline.ApiService;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    public async Task<string> GetAlbumRawAsync()
    {
        HttpResponseMessage  response = await _client.GetAsync("albums");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetUserRawAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("users");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}