using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Models
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>() 
            {
                new CityDto()
                {
                    Id = 1,
                    Name="El Paso",
                    Description="City descript",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="Point of int 1",
                            Description="Descrip of point of int 1"
                        },
                        new PointOfInterestDto()
                        {
                            Id=2,
                            Name="Point of int 2",
                            Description="Descrip of point of int 2"
                        },
                        new PointOfInterestDto()
                        {
                            Id=3,
                            Name="Point of int 3",
                            Description="Descrip of point of int 3"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name="New York",
                    Description="City descript",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=4,
                            Name="Point of int 4",
                            Description="Descrip of point of int 4"
                        },
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name="Point of int 5",
                            Description="Descrip of point of int 5"
                        },
                        new PointOfInterestDto()
                        {
                            Id=6,
                            Name="Point of int 6",
                            Description="Descrip of point of int 6"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name="Topochico",
                    Description="City descript",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=7,
                            Name="Point of int 7",
                            Description="Descrip of point of int 7"
                        },
                        new PointOfInterestDto()
                        {
                            Id=8,
                            Name="Point of int 7",
                            Description="Descrip of point of int 7"
                        },
                        new PointOfInterestDto()
                        {
                            Id=9,
                            Name="Point of int 7",
                            Description="Descrip of point of int 7"
                        }
                    }
                }
            };

        }
    }
}
