using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using TestApp1.Controllers;

namespace TestApp1.Views
{
    public class ApplicationInterface
    {
        private string UserAnswer { get; set; }
        private string CountryName { get; set; }
        public ApplicationInterface()
        {
            while (true)
            {
                Console.WriteLine("show — get country data from json;\nall — show all saved countries\nq — exit.\n");
                UserAnswer = Console.ReadLine();
                if (UserAnswer == "show")
                {
                    Console.WriteLine("Type country name:\n");
                    CountryName = Console.ReadLine();
                    var showCountry = new CountryData();
                    showCountry.ShowCountryDataFromJson(CountryName);
                    if (showCountry.countryExist == true)
                    {
                        Console.WriteLine("\nDo you want to save it to the database? Y/n");
                        if(Console.ReadLine() == "Y")
                        {
                            var addCountry = new CountriesHandler();
                            addCountry.NewCountry(CountryName);
                            Console.WriteLine("Country added");
                        }
                        else if (Console.ReadLine() == "n")
                        {
                            Console.WriteLine("Press enter to return to main menu");
                            Console.ReadLine();
                        }
                    }
                }
                else if (UserAnswer == "all")
                {
                    var dbCountries = new DataBaseCountries();
                    dbCountries.PrintAllCountries();
                }
                else if (UserAnswer == "q")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("There is no command " + UserAnswer + "\n");
                }
            }
        }
    }
}