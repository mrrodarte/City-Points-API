using MyNETCoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        Task<IEnumerable<City>> GetCitiesAsync();

        City GetCity(int cityId, bool includePointsOfInt = false);

        Task<City> GetCityAsync(int cityId, bool includePointsOfInt=false);

        Task<IEnumerable<PointOfInterest>>  GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest> GetPointOfInterestForCityAsync(int cityId, int pointOfIntId);

        Task<bool> CityExistsAsync(int cityId);

        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        Task DeletePointOfInterestAsync(PointOfInterest pointOfInterest);

        Task<bool> SaveAsync();
    }
}
