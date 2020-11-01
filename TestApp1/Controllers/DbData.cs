using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApp1.Models;

namespace TestApp1.Controllers
{
    public class DataBaseCountries
    {
        private List<Country> ListOfCountries { get; set; }
        private void GetAllCountries()
        {
            try
            {
                using var db = new CountryContext();
                ListOfCountries = db.Countries
                    .Include(city => city.Capital)
                    .Include(region => region.Region)
                    .ToList();
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "While getting data from the db");
            }
        }
        public void PrintAllCountries()
        {
            GetAllCountries();
            if (ListOfCountries != null)
            {
                foreach (var i in ListOfCountries)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", i.Name, i.Code, i.Capital.Name, i.Area, i.Population, i.Region.Name);
                }
            }
            Console.WriteLine();
        }
    }

    public class CountriesHandler
    {
        public int? CityID { get; set; }
        public int? RegionID { get; set; }
        private void CitiesHandler(CountryData countryData)
        {
            try
            {
                using var db = new CountryContext();
                if (db.Cities
                    .Any(city => city.Name.Equals(countryData.countryCapital)))
                {
                    CityID = db.Cities
                        .Where(city => city.Name.Equals(countryData.countryCapital))
                        .Select(city => city.Id)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "Handle Cities");
            }
        }
        private void RegionsHandler(CountryData countryData)
        {
            using var db = new CountryContext();
            try
            {
                if (db.Regions
                .Any(region => region.Name.Equals(countryData.countryRegion)))
                {
                    RegionID = db.Regions
                        .Where(region => region.Name.Equals(countryData.countryRegion))
                        .Select(region => region.Id)
                        .FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "Handle Regions");
            }
        }
        private void AddCountryHandler(CountryData countryData)
        {
            using var db = new CountryContext();
            try
            {
                db.Countries.Add(new Country
                {
                    Name = countryData.countryName,
                    Code = countryData.countryCode,
                    Area = countryData.countryArea,
                    Population = countryData.countryPopulation,
                    Capital = CityID == null
                    ? (new City() { Name = countryData.countryCapital })
                    : db.Cities.Where(city => city.Name.Equals(countryData.countryCapital)).FirstOrDefault(),
                    Region = RegionID == null
                    ? (new Region() { Name = countryData.countryRegion })
                    : db.Regions.Where(region => region.Name.Equals(countryData.countryRegion)).FirstOrDefault()
                });
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "While adding a new country");
            }
        }
        private void UpdateCountryHandler(CountryData countryData)
        {
            try
            {
                using var db = new CountryContext();
                var Country = db.Countries.Where(country => country.Code.Equals(countryData.countryCode)).FirstOrDefault();
                db.Countries.Update(Country);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "While update data");
            }
        }
        public void NewCountry(string countryName)
        {
            var countryData = new CountryData();
            countryData.GetData(countryName);
            RegionsHandler(countryData);
            CitiesHandler(countryData);
            using var db = new CountryContext();
            try
            {
                if (!db.Countries
                .Any(country => country.Code.Equals(countryData.countryCode)))
                {
                    AddCountryHandler(countryData);
                }
                else
                {
                    UpdateCountryHandler(countryData);
                }
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "Saving data");
            }
        }
    }
}