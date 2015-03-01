using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// Nuget - Web Api 2.2, System.Net.HTTP, JSON, HTTP Client libraries

namespace DVDRentalClient {
    class Client {
        static async Task DoWork() {
            try {
                using (HttpClient client = new HttpClient()) {

                    client.BaseAddress = new Uri("http://localhost:4622/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET all dvd information
                    // GET ../dvd
                    HttpResponseMessage response = await client.GetAsync("/dvd");
                    if (response.IsSuccessStatusCode) {
                        var dvds = await response.Content.ReadAsAsync<IEnumerable<DVD>>();
                        Console.WriteLine("\n-------Weather info for all cities-------------");
                        foreach (var d in dvds) {
                            Console.WriteLine(d.Title + " " + d.AgeRating + " " + d.RunningTime + " " + d.RentalDate + " " + d.ReleaseDate);
                        }
                    }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // GET based on title parameter
                    // GET ../dvd/title/harrypotter
                    DVD dvd = new DVD();
                    response = await client.GetAsync("/dvd/title/harrypotter");
                    if (response.IsSuccessStatusCode) {
                        dvd = await response.Content.ReadAsAsync<DVD>();
                        Console.WriteLine("\n-------Only Harry Potter information-------------");
                        Console.WriteLine(dvd.Title + " " + dvd.AgeRating + " " + dvd.RunningTime + " " + dvd.RentalDate + " " + dvd.ReleaseDate);
                    }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // GET based on release date parameter
                    // GET ../dvd/releasedate/2013/06/01
                    dvd = new DVD();
                    response = await client.GetAsync("/dvd/releasedate/2013/06/01");
                    if (response.IsSuccessStatusCode) {
                        var dvds = await response.Content.ReadAsAsync<IEnumerable<DVD>>();
                        Console.WriteLine("\n-------Only 2013/06/01 release date-------------");
                        foreach (var d in dvds) {
                            Console.WriteLine(d.Title + " " + d.AgeRating + " " + d.RunningTime + " " + d.RentalDate + " " + d.ReleaseDate);
                        }
                    }
                    else {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    // UPDATE Age rating
                    // PUT ../dvd/update/harrypotter
                    dvd = new DVD() { Title = "Harry Potter", RunningTime = 90, AgeRating = 18, ReleaseDate = new DateTime(2011, 6, 1), RentalDate = new DateTime() };
                    response = await client.PutAsJsonAsync("/dvd/update/harrypotter", dvd);
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------Update Rental Date-------------"); ;
                        Console.WriteLine(response.StatusCode + " Update Successful");
                    }

                    // ADD dvd
                    // POST ../dvd/addDVD
                    dvd = new DVD() { Title = "Happy", RunningTime = 90, AgeRating = 12, ReleaseDate = new DateTime(2011, 6, 1), RentalDate = new DateTime() };
                    response = await client.PostAsJsonAsync("/dvd/addDVD", dvd);
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------DVD added -------------"); ;
                        Console.WriteLine(response.StatusCode + " Update Successful");
                    }


                    // DELETE city
                    // DELETE ../weather/delete/clare
                    response = await client.DeleteAsync("/dvd/delete/2013/03/17");
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    else {
                        Console.WriteLine("\n-------Delete DVD-------------"); ;
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
