using AnimalPicturesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPicturesApi.Controllers;

[ApiController]
[Route("animal-pictures")]
public class AnimalPicturesController : ControllerBase
{
    private readonly ILogger<AnimalPicturesController> _logger;

    public AnimalPicturesController(ILogger<AnimalPicturesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{animalType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public string GetRecentPictures(AnimalType animalType)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public void FetchAndSaveImage(AnimalType animalType, int count)
    {
        throw new NotImplementedException();
    }
}