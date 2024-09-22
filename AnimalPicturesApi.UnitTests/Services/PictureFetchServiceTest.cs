using AnimalPicturesApi.Models;
using AnimalPicturesApi.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AnimalPicturesApi.UnitTests.Services;

public class PictureFetchServiceTest
{
    private readonly PictureFetchService _sut;
    private readonly Mock<ILogger<PictureFetchService>> _mockLogger;
    private readonly Mock<IAnimalImageApiService> _imageApiServiceMock;
    
    public PictureFetchServiceTest()
    {
        _mockLogger = new Mock<ILogger<PictureFetchService>>();
        _imageApiServiceMock = new Mock<IAnimalImageApiService>();
        _sut = new PictureFetchService(_mockLogger.Object, _imageApiServiceMock.Object);
    }
    
    [Fact]
    public async void When_AnimalType_Is_CAT_GetAnimalImagesByType_Should_Invoke_GetCatImages_Once()
    {
        //given
        var apiResponse = new List<CatApiResponse>()
        {
            new CatApiResponse() { id = "1", url = "url1", height = 100, width = 100}
        };
        
        _imageApiServiceMock.Setup(i => i.GetCatImages(It.IsAny<int>()))
            .ReturnsAsync(apiResponse);
        
        //when
        var result = await _sut.GetAnimalPicturesByType(AnimalType.CAT, 1);
        
        //then
        _imageApiServiceMock.Verify(i => i.GetCatImages(It.IsAny<int>()), Times.Once);
    }
}