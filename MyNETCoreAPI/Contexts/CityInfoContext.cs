using Microsoft.EntityFrameworkCore;
using MyNETCoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNETCoreAPI.Contexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //seed/populate the data
            modelBuilder.Entity<City>()
                .HasData(
                        new City()
                        {
                            Id = 1,
                            Name = "El Paso",
                            Description = "Southwest desert city",
                        },
                        new City()
                        {
                            Id = 2,
                            Name = "Juarez",
                            Description = "Heroic city in mexico",
                        },
                        new City()
                        {
                            Id = 3,
                            Name = "Chicago",
                            Description = "City of futures market",
                        }, new City()
                        {
                            Id = 4,
                            Name = "New York",
                            Description = "Home of Wallstreet",
                        }
                );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                        new PointOfInterest()
                        {
                            Id = 1,
                            CityId = 1,
                            Name = "Transmountain scenic park",
                            Description = "Descrip of transmountain"
                        },
                        new PointOfInterest()
                        {
                            Id = 2,
                            CityId = 2,
                            Name = "Bars and Restaurants",
                            Description = "Descrip of point of int Juarez"
                        },
                        new PointOfInterest()
                        {
                            Id = 3,
                            CityId = 3,
                            Name = "The futures market building",
                            Description = "Descrip of point of int Chicago"
                        },
                        new PointOfInterest()
                        {
                            Id = 4,
                            CityId = 4,
                            Name = "NYSE building",
                            Description = "Wallstreet building"
                        },
                        new PointOfInterest()
                        {
                            Id = 5,
                            CityId = 4,
                            Name = "Manhatan",
                            Description = "City of Manhatan"
                        }
                );
            base.OnModelCreating(modelBuilder);
        }

        // one way of setting up connection string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
