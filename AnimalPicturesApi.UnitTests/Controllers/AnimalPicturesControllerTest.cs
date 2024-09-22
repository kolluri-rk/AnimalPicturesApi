using AnimalPicturesApi.Controllers;
using AnimalPicturesApi.Models;
using AnimalPicturesApi.Repositories;
using AnimalPicturesApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnimalPicturesApi.UnitTests.Controllers;

public class AnimalPicturesControllerTest
{
    private readonly AnimalPicturesController _sut;
    private readonly Mock<ILogger<AnimalPicturesController>> _mockLogger;
    private readonly Mock<IPictureFetchService> _mockFetchService;
    private readonly Mock<IAnimalsPicturesRepository> _mockAnimalsRepository;

    public AnimalPicturesControllerTest()
    {
        _mockLogger = new Mock<ILogger<AnimalPicturesController>>();
        _mockFetchService = new Mock<IPictureFetchService>();
        _mockAnimalsRepository = new Mock<IAnimalsPicturesRepository>();
        _sut = new AnimalPicturesController(_mockLogger.Object, _mockFetchService.Object, _mockAnimalsRepository.Object);
    }

    [Fact]
    public async void GetRecentPicture_Invokes_AnimalRepository_Once()
    {
        //given
        _mockAnimalsRepository
            .Setup(r => r.GetRecenPictureByType(It.IsAny<AnimalType>()))
            .Returns(It.IsAny<AnimalPicture>());
        
        //when
        var response = await _sut.GetRecentPicture(AnimalType.CAT);

        //then
        _mockAnimalsRepository
            .Verify(r => r.GetRecenPictureByType(AnimalType.CAT), Times.Once);
    }
    
    [Fact]
    public async void GetRecentPicture_Retruns_Image_From_AnimalRepository()
    {
        //given
        var image = new AnimalPicture() {Id = 1, Url = "url"};
        _mockAnimalsRepository
            .Setup(r => r.GetRecenPictureByType(It.IsAny<AnimalType>()))
            .Returns(image);
        
        //when
        var response = await _sut.GetRecentPicture(AnimalType.CAT);

        //then
        response.Result.Should().BeOfType<OkObjectResult>();
        var okResult = response.Result as OkObjectResult;
        okResult.Value.Should().Be(image);
    }
    
    [Fact]
    public async void FetchAndSaveImage_Invokes_FetchService_And_AnimalsRepository_Once()
    {
        //given
        _mockFetchService
            .Setup(f => f.GetAnimalPicturesByType(It.IsAny<AnimalType>(), It.IsAny<int>()))
            .ReturnsAsync(It.IsAny<List<AnimalPicture>>);
        
        //when
        var response = await _sut.FetchAndSaveImage(AnimalType.CAT);
        
        //then
        _mockFetchService.Verify(f => f.GetAnimalPicturesByType(It.IsAny<AnimalType>(), It.IsAny<int>()), Times.Once);
        _mockAnimalsRepository.Verify(r => r.Save(It.IsAny<List<AnimalPicture>>()), Times.Once);
    }
}