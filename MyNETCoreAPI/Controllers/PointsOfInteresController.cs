using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNETCoreAPI.Models;
using MyNETCoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInteresController:ControllerBase
    {
        private readonly ILogger<PointsOfInteresController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepo;
        private readonly IMapper _mapper;

        public PointsOfInteresController(ILogger<PointsOfInteresController> logger,
            IMailService mailService,ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepo = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetPointsOfInterest(int cityId)
        {

            if (! await _cityInfoRepo.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found or value returned null.");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepo.GetPointsOfInterestForCityAsync(cityId); //CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{id}",Name ="GetPointOfInterest")]
        public async Task<IActionResult> GetPointOfInterest(int cityId, int id)
        {
            if (! await _cityInfoRepo.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found or value returned null.");
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepo.GetPointOfInterestForCityAsync(cityId, id);

            if (pointOfInterest == null)
                return NotFound();

           
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
            //return new JsonResult(CitiesDataStore.Current.Cities.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePointOfInterest(int cityId,
            [FromBody]PointOfInterestForCreationDto pointOfInterest)
        {

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description", "Description and Name cannot be the same.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _cityInfoRepo.CityExistsAsync(cityId))
                return NotFound();

            if (pointOfInterest == null)
                return BadRequest();


            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepo.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);
            await _cityInfoRepo.SaveAsync();

            var createdPoIToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", 
                                    new { cityId, id = createdPoIToReturn.Id },
                                    createdPoIToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description", "Description and Name cannot be the same.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (! await _cityInfoRepo.CityExistsAsync(cityId))
                return NotFound();


            var pointOfInterestEntity =  await _cityInfoRepo.GetPointOfInterestForCityAsync(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();

            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            await _cityInfoRepo.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialPointOfInterest(int cityId,int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {

            if (! await _cityInfoRepo.CityExistsAsync(cityId))
                return NotFound();

            var pointOfInterestEntity = await _cityInfoRepo
                .GetPointOfInterestForCityAsync(cityId, id);

            if (pointOfInterestEntity == null)
                return NotFound();

            var pointOfInterestToPatch = _mapper
                .Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

           

            patchDoc.ApplyTo(pointOfInterestToPatch,ModelState);

            //validates the patch document
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError(
                    "Description", "Description and Name cannot be the same.");
            }

            //validates the dto to be patched
            if (!TryValidateModel(pointOfInterestToPatch))
                return BadRequest(ModelState);

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepo.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointOfInterest(int cityId, int id)
        {
            if (! await _cityInfoRepo.CityExistsAsync(cityId))
                return NotFound();

            var pointOfInterestEntity = _cityInfoRepo
                .GetPointOfInterestForCityAsync(cityId, id).Result;

            if (pointOfInterestEntity == null)
                return NotFound();

            await _cityInfoRepo.DeletePointOfInterestAsync(pointOfInterestEntity);

            //send mock email
            _mailService.Send("Point of interest was deleted.",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted/removed.");

            return NoContent();
        }
    }
}
