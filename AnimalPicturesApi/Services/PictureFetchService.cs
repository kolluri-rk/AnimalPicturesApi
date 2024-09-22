using AnimalPicturesApi.Models;
using Newtonsoft.Json;

namespace AnimalPicturesApi.Services;

public class PictureFetchService : IPictureFetchService
{
    private readonly ILogger<PictureFetchService> _logger;
    private readonly IAnimalImageApiService _animalImageApiService;

    public PictureFetchService(
        ILogger<PictureFetchService> logger, 
        IAnimalImageApiService animalImageApiService)
    {
        _logger = logger;
        _animalImageApiService = animalImageApiService;
    }
    
    public async Task<List<AnimalPicture>> GetAnimalPicturesByType(AnimalType animalType, int count)
    {
        var fetchedImages = await _animalImageApiService.GetCatImages(count);
        
        var animalImages = fetchedImages!.ConvertAll(fi => 
            new AnimalPicture() { AnimalType = AnimalType.CAT.ToString(), Url = fi.url });
        _logger.Log(LogLevel.Debug, "Animal Images: {%}", JsonConvert.SerializeObject(animalImages));

        return animalImages;
    }
}