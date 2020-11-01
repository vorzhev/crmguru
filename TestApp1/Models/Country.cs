using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp1.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public City Capital { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
        public Region Region { get; set; }
    }
}
