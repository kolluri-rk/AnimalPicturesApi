using AnimalPicturesApi.Models;

namespace AnimalPicturesApi.Repositories;

public interface IAnimalsPicturesRepository
{
    void Save(List<AnimalPicture> images);
    
    AnimalPicture GetRecenPictureByType(AnimalType type);
}