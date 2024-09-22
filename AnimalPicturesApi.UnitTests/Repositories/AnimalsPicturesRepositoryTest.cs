using AnimalPicturesApi.Models;
using AnimalPicturesApi.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AnimalPicturesApi.UnitTests.Repositories;

public class AnimalsPicturesRepositoryTest
{
    private readonly AnimalsDbContext _context;
    private readonly AnimalsPicturesRepository _sut;

    public AnimalsPicturesRepositoryTest()
    {
        var _contextOptions = new DbContextOptionsBuilder<AnimalsDbContext>()
            .UseInMemoryDatabase("BloggingControllerTest")
            .Options;

        _context = new AnimalsDbContext(_contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        
        _sut = new AnimalsPicturesRepository(_context);
    }

    [Fact]
    public void GetRecenPictureByType_Should_Return_Recent_Picture()
    {
        //given
        var image1 = new AnimalPicture() { Id = 1, AnimalType = "CAT", Url = "url1"};
        var image2 = new AnimalPicture() { Id = 2, AnimalType = "CAT", Url = "url2"};
        var image3 = new AnimalPicture() { Id = 3, AnimalType = "DOG", Url = "url3"};
        _context.AnimalPictures.AddRange(new List<AnimalPicture>() { image1, image2, image3 });
        _context.SaveChanges();
        
        //when
        var result = _sut.GetRecenPictureByType(AnimalType.CAT);
        
        //then
        result.Should().BeEquivalentTo(image2);
    }
}