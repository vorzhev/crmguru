using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TestApp1.Models;
using Microsoft.IdentityModel.Protocols;

namespace TestApp1.Controllers
{
    public class CountryContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public CountryContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch(Exception ex)
            {
                ExceptionsHandler.CallExceptionMessage(ex);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
    }
}