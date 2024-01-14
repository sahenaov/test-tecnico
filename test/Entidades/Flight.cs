using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Entidades

{
    public class Flight
    {
        public Transport Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }

        public double Price { get; set; }

        public double PriceCOP { get; set; }
    }
}
