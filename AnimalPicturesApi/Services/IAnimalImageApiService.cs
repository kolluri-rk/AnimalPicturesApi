using AnimalPicturesApi.Models;

namespace AnimalPicturesApi.Services;

public interface IAnimalImageApiService
{
    Task<List<CatApiResponse>?> GetCatImages(int count);
}