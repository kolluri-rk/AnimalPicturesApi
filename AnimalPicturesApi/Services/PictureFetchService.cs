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
        var animalImages = new List<AnimalPicture>();
        
        switch (animalType)
        {
            case AnimalType.CAT:
            {
                var fetchedImages = await _animalImageApiService.GetCatImages(count);
                animalImages = fetchedImages!.ConvertAll(fi => new AnimalPicture() { AnimalType = AnimalType.CAT.ToString(), Url = fi.url });
                break;
            }
            case AnimalType.DOG:
            {
                var fetchedImages = await _animalImageApiService.GetDogImages(count);
                animalImages = fetchedImages!.ConvertAll(fi => new AnimalPicture() { AnimalType = AnimalType.DOG.ToString(), Url = fi.message });
                break;
            }
            default: throw new NotImplementedException("Unknown animal type");
        }
        
        return animalImages;
    }
}