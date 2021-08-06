using AutoMapper;
using MyNETCoreAPI.Entities;
using MyNETCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityWithOutPoIDto>();
            CreateMap<City, CityDto>();
        }
    }
}
