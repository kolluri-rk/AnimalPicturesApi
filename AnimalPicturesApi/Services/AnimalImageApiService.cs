using AnimalPicturesApi.Models;
using Newtonsoft.Json;

namespace AnimalPicturesApi.Services;

public class AnimalImageApiService : IAnimalImageApiService
{
    private readonly ILogger<AnimalImageApiService> _logger;
    private readonly HttpClient _httpClient;

    private const string CAT_API_BASE_URL = "https://api.thecatapi.com/v1/images";

    public AnimalImageApiService(ILogger<AnimalImageApiService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<List<CatApiResponse>?> GetCatImages(int count)
    {
        var fetchedImages = (count == 1) ?
            await _httpClient.GetFromJsonAsync<List<CatApiResponse>>($"{CAT_API_BASE_URL}/search") :
            await _httpClient.GetFromJsonAsync<List<CatApiResponse>>($"{CAT_API_BASE_URL}/search?limit={count}");
        
        _logger.Log(LogLevel.Debug, "Cat API response: {%}", JsonConvert.SerializeObject(fetchedImages));

        return fetchedImages;
    }
}