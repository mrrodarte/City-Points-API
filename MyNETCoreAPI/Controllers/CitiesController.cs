using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyNETCoreAPI.Models;
using MyNETCoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepo;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepo,IMapper mapper)
        {
            _cityInfoRepo = cityInfoRepo ?? throw new ArgumentNullException(nameof(cityInfoRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cityEntities = await _cityInfoRepo.GetCitiesAsync();

            var results = new List<CityWithOutPoIDto>();

            //foreach(var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithOutPoIDto()
            //    {
            //        Id = cityEntity.Id,
            //        Description = cityEntity.Description,
            //        Name = cityEntity.Name
            //    });
            //}   

            //use a mapper instead of foreach above
            return Ok(_mapper.Map<IEnumerable<CityWithOutPoIDto>>(cityEntities));
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCity(int id,bool includePoI = false)
        {
            var cityToReturn = await _cityInfoRepo.GetCityAsync(id,includePoI); //CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (cityToReturn == null)
                return NotFound();

            if (includePoI)
            {
                return Ok(_mapper.Map<CityDto>(cityToReturn));
            }
            else
            {
                var cityWithOutPoiResult = new CityWithOutPoIDto()
                {
                    Id = cityToReturn.Id,
                    Name = cityToReturn.Name,
                    Description = cityToReturn.Description
                };

                return Ok(_mapper.Map<CityWithOutPoIDto>(cityToReturn));
            }
            //return new JsonResult(CitiesDataStore.Current.Cities.Where(x => x.Id == id).FirstOrDefault());
        }

        [Route("api/[controller]sync/{id}")]
        [HttpGet]
        public IActionResult GetCitySync(int id, bool includePoI = false)
        {
            var cityToReturn = _cityInfoRepo.GetCity(id, includePoI); //CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (cityToReturn == null)
                return NotFound();

            if (includePoI)
            {
                return Ok(_mapper.Map<CityDto>(cityToReturn));
            }
            else
            {
                var cityWithOutPoiResult = new CityWithOutPoIDto()
                {
                    Id = cityToReturn.Id,
                    Name = cityToReturn.Name,
                    Description = cityToReturn.Description
                };

                return Ok(_mapper.Map<CityWithOutPoIDto>(cityToReturn));
            }
            //return new JsonResult(CitiesDataStore.Current.Cities.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}
