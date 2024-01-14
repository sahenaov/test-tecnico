using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using test.Entidades;

namespace test.Models
{
    public class FlightData
    {
        public class FlightDataAccess
        {
            private readonly HttpClient _httpClient;

            public FlightDataAccess()
            {
                _httpClient = new HttpClient();
               
                _httpClient.BaseAddress = new Uri("https://recruiting-api.newshore.es/api/flights/1");
            }

            public async Task<List<Flight>> GetFlights(int id)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{id}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var flights = JsonConvert.DeserializeObject<List<Flight>>(content);
                    return flights;
                }
                catch (HttpRequestException ex)
                {
                    // Log the exception or handle it according to your application's requirements.
                    throw new Exception("Error al obtener los vuelos de la API", ex);
                }
            }
        }
    }
}
