using SampleCA1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleCA1.Controllers
{
    public class WeatherController : ApiController
    {
        // Attribute based routing

        [RoutePrefix("weather")]
        public class WeatherController : ApiController {
            // Collection for WeatherInfo
            private static List<WeatherInfo> weather = new List<WeatherInfo>(){
            new WeatherInfo{ City = "Clare", Temperature = 12, WindSpeed = 5, WeatherConditions= "Sunny", WeatherWarnings = false },
            new WeatherInfo{ City = "Dublin", Temperature = 19, WindSpeed = 15, WeatherConditions= "Windy", WeatherWarnings = true },
            new WeatherInfo{ City = "Meath", Temperature = 2, WindSpeed = 12, WeatherConditions= "Rainy", WeatherWarnings = false }
        };

            // GET /weather
            // Returns all the weather information for the list
            [Route("")]
            [HttpGet]
            public IHttpActionResult RetrieveAllWeatherInfo() {
                return Ok(weather);
            }

            // GET /weather/city/Dublin
            // {parameter:constraint}
            [Route("city/{city:alpha}")]
            public IHttpActionResult GetWeatherInformation(String city) {
                // Use LINQ to find the weather for a given city
                WeatherInfo cityWeather = weather.FirstOrDefault(w => w.City.ToUpper() == city.ToUpper());
                if (cityWeather == null) {
                    return NotFound();      // 404 Not Found
                }
                return Ok(city);            // 200 OK
            }
        }
    }
}
