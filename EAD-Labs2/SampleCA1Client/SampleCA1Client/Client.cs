using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SampleCA1Client.Models;

namespace SampleCA1Client {
    class Client {
        static async Task DoWork() {
            try {
                using (HttpClient client = new HttpClient()) {

                    client.BaseAddress = new Uri("http://localhost:1538/"); 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET all weather information
                    // GET ../weather
                    HttpResponseMessage response = await client.GetAsync("/weather"); 
                    if (response.IsSuccessStatusCode) {
                        var weather = await response.Content.ReadAsAsync<IEnumerable<WeatherInfo>>();
                        Console.WriteLine("\n-------Weather info for all cities-------------");
                        foreach (var w in weather) {
                            Console.WriteLine(w.City + " " + w.Temperature + "C " + w.WindSpeed + "km/h " + w.WeatherConditions + " warning: " + w.WeatherWarnings);
                        }
                    }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // GET based on City parameter
                    // GET ../weather/city/Dublin
                    WeatherInfo weatherInfo = new WeatherInfo();
                    response = await client.GetAsync("/weather/city/Dublin");
                    if (response.IsSuccessStatusCode) {
                        weatherInfo = await response.Content.ReadAsAsync<WeatherInfo>();
                        Console.WriteLine("\n-------Only Dublin information-------------");
                        Console.WriteLine(weatherInfo.City + " " + weatherInfo.Temperature + "C "
                            + weatherInfo.WindSpeed + "km/h " + weatherInfo.WeatherConditions + " warning: " + weatherInfo.WeatherWarnings);
                        }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // GET cities with weather warnings
                    // GET ../weather/city/warning/true
                    response = client.GetAsync("weather/city/warning/true").Result;
                    if (response.IsSuccessStatusCode) {
                        var cities = await response.Content.ReadAsAsync<IEnumerable<String>>();
                        Console.WriteLine("\n-------True weather warnings-------------"); 
                        foreach (String city in cities) {
                            Console.WriteLine(city);
                        }
                    }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // ADD city
                    // POST ../weather/addCity
                    weatherInfo = new WeatherInfo() { City = "Paris", WeatherConditions = "Cold", Temperature = 20.2, WindSpeed = 10, WeatherWarnings = true };
                    response = await client.PostAsJsonAsync("/weather/addCity", weatherInfo);
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------City added weather-------------"); ;
                        Console.WriteLine(response.StatusCode + " Update Successful");
                    }


                    // UPDATE city weather
                    // PUT ../weather/Update/Dublin
                    weatherInfo = new WeatherInfo() { City = "Dublin", WeatherConditions = "Bleedin Beautiful", Temperature = 2, WindSpeed = 10, WeatherWarnings = false };
                    response = await client.PutAsJsonAsync("/weather/Update/Dublin", weatherInfo);
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------Update Dublin weather-------------"); ;
                        Console.WriteLine(response.StatusCode + " Update Successful");
                    }

                    // DELETE city
                    // DELETE ../weather/delete/clare
                    response = await client.DeleteAsync("/weather/Delete/Clare");
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------Delete Dublin weather-------------"); ;
                        Console.WriteLine(response.StatusCode + " Delete Successful");
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args) {
            DoWork().Wait();
            Console.ReadLine();
        }
    }
}
