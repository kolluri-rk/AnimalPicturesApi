using AnimalPicturesApi.Models;
using AnimalPicturesApi.Repositories;
using AnimalPicturesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPicturesApi.Controllers;

[ApiController]
[Route("animal-pictures")]
public class AnimalPicturesController : ControllerBase
{
    private readonly ILogger<AnimalPicturesController> _logger;
    private readonly IPictureFetchService _animalPicturesService;
    private readonly IAnimalsPicturesRepository _animalsPicturesRepository;

    public AnimalPicturesController(
        ILogger<AnimalPicturesController> logger, 
        IPictureFetchService animalPicturesService, 
        IAnimalsPicturesRepository animalsPicturesRepository)
    {
        _logger = logger;
        _animalPicturesService = animalPicturesService;
        _animalsPicturesRepository = animalsPicturesRepository;
    }

    [HttpGet("{animalType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AnimalPicture>> GetRecentPicture(AnimalType animalType)
    {
        var picture = _animalsPicturesRepository.GetRecenPictureByType(animalType);
        return Ok(picture);
    }

    [HttpPost("{animalType}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> FetchAndSaveImage(AnimalType animalType, int count = 1)
    {
        //fetch
        var pictures = await _animalPicturesService.GetAnimalPicturesByType(animalType, count);
        
        //store
        _animalsPicturesRepository.Save(pictures);
        
        //return status
        return Ok();
    }
}