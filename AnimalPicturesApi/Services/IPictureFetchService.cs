using AnimalPicturesApi.Models;

namespace AnimalPicturesApi.Services;

public interface IPictureFetchService
{
    public Task<List<AnimalPicture>> GetAnimalPicturesByType(AnimalType animalType, int count);
}