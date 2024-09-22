using AnimalPicturesApi.Models;

namespace AnimalPicturesApi.Repositories;

public class AnimalsPicturesRepository : IAnimalsPicturesRepository
{
    private AnimalsDbContext _context;
    
    public AnimalsPicturesRepository(AnimalsDbContext context)
    {
        this._context = context;
    }

    public void Save(List<AnimalPicture> images)
    {
        _context.AnimalPictures.AddRange(images);
        _context.SaveChanges();
    }

    public AnimalPicture GetRecenPictureByType(AnimalType type)
    {
        return _context.AnimalPictures
            .Where(p => p.AnimalType.ToLower() == type.ToString().ToLower())
            .OrderByDescending(ai => ai.Id)
            .FirstOrDefault()!;
    }
}