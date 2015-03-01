using SampleCA1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleCA1.Controllers
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
            // The weather information for all cities
            [Route("")]
            [HttpGet]
            public IHttpActionResult RetrieveAllWeatherInfo() {
                return Ok(weather);
            }

            // GET /weather/city/Dublin
            // {parameter:constraint}
            // The weather information for a specified city
            [Route("city/{city:alpha}", Name="GetWeather")]
            public IHttpActionResult GetWeatherInformation(String city) {
                // Use LINQ to find the weather for a given city
                WeatherInfo cityWeather = weather.FirstOrDefault(w => w.City.ToUpper() == city.ToUpper());
                if (cityWeather == null) {
                    return NotFound();      // 404 Not Found
                }
                return Ok(cityWeather);            // 200 OK
            }

            [Route("city/warning/{warning:bool}")]
            // GET weather/city/warning/true or false
            // The cities for a specified weather warning status
            public IHttpActionResult GetCityNameForWeatherWarning(bool warning) {
                var cities = weather.Where(w => w.WeatherWarnings == warning).Select(w => w.City);
                return Ok(cities);
            }

            [Route("Update/{cityName:alpha}")]
            // UPDATE weather/update/dublin
            // The weather information for a specified city to be updated
            public IHttpActionResult PutWeatherWarning(String cityName, WeatherInfo weatherInfo) {
                if (cityName != null) {
                    WeatherInfo we = weather.SingleOrDefault(c => c.City.ToLower() == cityName.ToLower());
                    var result = weather.SingleOrDefault(c => c.City.ToLower() == cityName.ToLower());
                    if (result != null) {
                        try {
                            if (ModelState.IsValid) {
                                weather.Remove(result);
                                weather.Add(weatherInfo);
                                return Ok();
                            }
                        }
                        catch (Exception e){
                            //  e.Message.ToString();
                            //  throw;
                        }
                    }
                    else {
                        return BadRequest();
                    }
                }
                return NotFound();
            }

            [Route("addCity")]
            // POST weather/addCity
            public IHttpActionResult PostCity(WeatherInfo weatherInfo) {
                if (ModelState.IsValid) {
                    var record = weather.FirstOrDefault(l => l.City.ToUpper() == weatherInfo.City.ToUpper());
                    if (record == null) {
                        weather.Add(weatherInfo);
                        String uri = Url.Link("GetWeather", new { City = weatherInfo.City });
                        return Created(uri, weatherInfo);
                    }
                    else {
                        return NotFound();
                    }
                }
                else {
                    return BadRequest();
                }
            }

            [Route("Delete/{cityName:alpha}")]
            // DELETE weather/delete/clare
            public IHttpActionResult DeleteCity(String cityName) {
                var result = weather.SingleOrDefault(c => c.City.ToLower().Equals(cityName.ToLower()));
                if (cityName != null) {
                    try {
                        if (ModelState.IsValid) {
                            weather.Remove(result);
                            return Ok();
                        }
                    }
                    catch (Exception e) {

                    }
                }
                return NotFound();
            }

    }
}

