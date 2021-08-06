using Microsoft.EntityFrameworkCore;
using MyNETCoreAPI.Contexts;
using MyNETCoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public City GetCity(int cityId, bool includePointsOfInt = false)
        {
            //DEBUG simulate throttling for asnyc performance test
            //_context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:05'");

            if (includePointsOfInt)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }
            else
            {
                return _context.Cities.FirstOrDefault(c => c.Id == cityId);
            }
        }

        public async Task<City> GetCityAsync(int cityId,bool includePointsOfInt=false)
        {
            //DEBUG simulate throttling for asnyc performance test
            //_context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:05'");

            if (includePointsOfInt)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
            }
        }

        public async Task<PointOfInterest> GetPointOfInterestForCityAsync(int cityId, int pointOfIntId)
        {
            return await _context.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == pointOfIntId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest); 
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task DeletePointOfInterestAsync(PointOfInterest pointOfInterest)
        {
            await Task.Run(() => _context.PointsOfInterest.Remove(pointOfInterest));
        }
    }
}
