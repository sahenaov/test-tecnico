using Microsoft.AspNetCore.Mvc;
using static test.Models.FlightData;
using test.Entidades;

namespace test.Controllers
{
    public class FlightBusiness : Controller
    {
        private readonly FlightDataAccess _flightDataAccess;

        public FlightBusiness(FlightDataAccess flightDataAccess)
        {
            _flightDataAccess = flightDataAccess;
        }

        public async Task<List<Journey>> GetTravelRoutes(string origin, string destination)
        {
            try
            {
                var allFlights = await _flightDataAccess.GetFlights(1);

                var possibleRoutes = SearchRoutes(allFlights, origin, destination);

                var journeyList = possibleRoutes
                    .Where(route => route != null) // Filtrar rutas nulas
                    .Select(route => new Journey
                    {
                        Origin = origin,
                        Destination = destination,
                        Flights = route
                            .Where(f => f != null) // Filtrar vuelos nulos
                            .Select(f => new Flight
                            {
                                Origin = f.DepartureStation,
                                Destination = f.ArrivalStation,
                                Price = f.Price,
                                Transport = f.Transport != null
                                    ? new Transport
                                    {
                                        FlightCarrier = f.Transport.FlightCarrier,
                                        FlightNumber = f.Transport.FlightNumber
                                    }
                                    : null
                            }).ToList(),
                        Price = CalculatePrice(route),
                        NumberOfFlights = route.Count(f => f != null), // Contar vuelos no nulos
                        Stops = route.Count(f => f != null) > 1 ? route.Count(f => f != null) - 1 : 0,
                    })
                    .ToList();




                return journeyList;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's requirements.
                throw new Exception("Error al calcular las rutas de viaje", ex);
            }
        }

        private List<List<Flight>> SearchRoutes(List<Flight> allFlights, string currentOrigin, string destination)
        {
            var routes = new List<List<Flight>>();
            SearchRoutesRecursive(allFlights, currentOrigin, destination, new List<Flight>(), routes);
            return routes;
        }

        private void SearchRoutesRecursive(List<Flight> allFlights, string currentOrigin, string destination, List<Flight> currentRoute, List<List<Flight>> allRoutes)
        {
            if (currentOrigin == destination)
            {
                // Avoid duplicate routes
                if (!allRoutes.Any(route => route.SequenceEqual(currentRoute)))
                {
                    allRoutes.Add(new List<Flight>(currentRoute));
                }
                return;
            }

            // Avoid duplicate flights in the current route
            var possibleFlights = allFlights
                .Where(f => f.DepartureStation == currentOrigin && !currentRoute.Contains(f))
                .ToList();

            foreach (var flight in possibleFlights)
            {
                currentRoute.Add(flight);
                SearchRoutesRecursive(allFlights, flight.ArrivalStation, destination, currentRoute, allRoutes);
                currentRoute.Remove(flight);
            }

            // Consider direct flights only if there are no stops in the current route
            if (!currentRoute.Any())
            {
                var directFlight = allFlights.FirstOrDefault(f => f.DepartureStation == currentOrigin && f.ArrivalStation == destination);
                if (directFlight != null && !currentRoute.Contains(directFlight) && !allRoutes.Any(route => route.SequenceEqual(new List<Flight> { directFlight })))
                {
                    allRoutes.Add(new List<Flight> { directFlight });
                }
            }
        }

        private double CalculatePrice(List<Flight> flights)
        {
            return flights.Sum(f => f.Price);
        }
    }
}
