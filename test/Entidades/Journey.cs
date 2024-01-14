using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Entidades
{
    public class Journey
    {
        public List<Flight> Flights { get; set; }
        public String Origin { get; set; }
        public String Destination { get; set;}

        public Double Price { get; set;}

        public int NumberOfFlights { get; set; } // Número de vuelos en la ruta
        public int Stops { get; set; } = 0;
    }
}