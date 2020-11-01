using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

using TestApp1.Models;

namespace TestApp1.Controllers
{
    public class FileContext
    {
        private string FileAddress { get; set; }
        private string DataAddress = "https://restcountries.eu/rest/v2/name/";
        public string DataText { get; set; }
        private void ReadFile(string address)
        {
            try
            {
                DataText = (new WebClient()).DownloadString(address);
            }
            catch (Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "Reading the JSON");
            }
        }
        public void GetJson(string countryName)
        {
            string jsonFullAddress = String.Format("{0}{1}", DataAddress, countryName);
            ReadFile(jsonFullAddress);
        }
    }

    public class JsonData
    {
        public List<Currency> modelCurrency { get; set; }
        public List<Language> modelLanguage { get; set; }
        public List<Translations> modelTranslations { get; set; }
        public List<JsonCountry> modelJsonCountry { get; set; }
        public List<Root> modelRoot { get; set; }

        public List<JsonCountry> FillCountries(string countryName)
        {
            var dataFile = new FileContext();
            dataFile.GetJson(countryName);
            try
            {
                modelRoot = JsonConvert.DeserializeObject<List<Root>>(dataFile.DataText);
                modelJsonCountry = JsonConvert.DeserializeObject<List<JsonCountry>>(dataFile.DataText);
                //modelTranslations = JsonConvert.DeserializeObject<List<Translations>>(dataFile.DataText);
                //modelLanguage = JsonConvert.DeserializeObject<List<Language>>(dataFile.DataText);
                //modelCurrency = JsonConvert.DeserializeObject<List<Currency>>(dataFile.DataText);
            }
            catch (Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex, "Converting data from the JSON");
            }
            return modelJsonCountry;
        }
    }
    public class CountryData
    {
        public string countryName { get; set; }
        public string countryCapital { get; set; }
        public string countryRegion { get; set; }
        public double countryArea { get; set; }
        public string countryCode { get; set; } 
        public int countryPopulation { get; set; }
        public bool? countryExist { get; set; }

        public void GetData(string nameOfCountry)
        {
            var showCountry = new JsonData();
            var getDataCountry = showCountry.FillCountries(nameOfCountry);

            if (getDataCountry != null)
            {
                countryName = getDataCountry[0].name;
                countryCapital = getDataCountry[0].capital;
                countryRegion = getDataCountry[0].region;
                countryArea = getDataCountry[0].area;
                countryCode = getDataCountry[0].alpha3Code;
                countryPopulation = getDataCountry[0].population;
                countryExist = true;
            }
            else countryExist = false;
        }
        public void ShowCountryDataFromJson(string nameOfCountry)
        {
            GetData(nameOfCountry);
            if (countryExist == true) {
                Console.WriteLine("\nCountry name: {0}", countryName);
                Console.WriteLine("Capital: {0}", countryCapital);
                Console.WriteLine("Region: {0}", countryRegion);
                Console.WriteLine("Area: {0}", countryArea);
                Console.WriteLine("ISO 3166-1: {0}", countryCode);
                Console.WriteLine("Population: {0}", countryPopulation);
            }
            else Console.WriteLine("There is no country named " + nameOfCountry + " or list is null\n");
        }
    }
}