using DVDRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DVDRental.Controllers {
    [RoutePrefix("dvd")]
    public class DVDController : ApiController {

        private static List<DVD> dvdlist = new List<DVD>() { 
            new DVD { Title = "Harry Potter", RunningTime = 90, AgeRating = 12, ReleaseDate = new DateTime(2013, 6, 1), RentalDate = new DateTime() },
            new DVD { Title = "Big Hero", RunningTime = 100, AgeRating = 15, ReleaseDate = new DateTime(2013, 6, 1),RentalDate = new DateTime() },
            new DVD { Title = "Alpha Dog", RunningTime = 120, AgeRating = 16, ReleaseDate = new DateTime(2013, 3, 17),RentalDate = new DateTime() },
            new DVD { Title = "Disturbia", RunningTime = 140, AgeRating = 18, ReleaseDate = new DateTime(2015, 8, 12),RentalDate = new DateTime() }
        };

        [Route("")]
        [HttpGet]
        // /dvd
        // GET all dvds
        public IHttpActionResult RetrieveAllDvd() {
            return Ok(dvdlist);
        }

        [Route("title/{title:alpha}", Name = "GetDvd")]
        // /dvd/title/harrypotter
        // GET a dvd by the title
        public IHttpActionResult GetDvdTitle(String title) {
            DVD dvdTitle = dvdlist.FirstOrDefault(d => d.Title.ToUpper().Replace(" ", "") == title.ToUpper().Replace(" ", null));
            if (dvdTitle == null) {
                return NotFound();
            }
            return Ok(dvdTitle);
        }

        [Route("releasedate/{*releasedate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("releasedate/{*releasedate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        // /dvd/releasedate/2013/06/01
        // GET all dvds with the release date 2013/06/01
        public IHttpActionResult GetDvdReleaseDate(DateTime releasedate) {
            var dvdReleaseDate = dvdlist.Where(d => DateTime.Compare(d.ReleaseDate.Date, releasedate.Date) == 0);
            if (dvdReleaseDate == null) {
                return NotFound();
            }
            return Ok(dvdReleaseDate);
        }

        [Route("update/{title:alpha}")]
        // /dvd/update/harrypotter
        // PUT update the age rating dvd of given title
        public IHttpActionResult PutWeatherWarning(String title, DVD dvd) {
            if (title != null) {
                var result = dvdlist.SingleOrDefault(c => c.Title.ToLower().Replace(" ", "") == title.ToLower().Replace(" ", ""));
                if (result != null) {
                    try {
                        if (ModelState.IsValid) {
                            dvdlist.Remove(result);
                            dvdlist.Add(dvd);
                            return Ok();
                        }
                    }
                    catch (Exception e) {
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

        [Route("addDVD")]
        // POST add a DVD
        // /dvd/addDVD
        public IHttpActionResult PostDvd(DVD dvd) {
            if (ModelState.IsValid) {
                var record = dvdlist.FirstOrDefault(l => l.Title.ToUpper().Replace(" ", "") == dvd.Title.ToUpper().Replace(" ", ""));
                if (record == null) {
                    dvdlist.Add(dvd);
                    String uri = Url.Link("GetDvd", new { title = dvd.Title });
                    return Created(uri, dvd);
                }
                else {
                    return NotFound();
                }
            }
            else {
                return BadRequest();
            }
        }

        [Route("delete/{*releaseDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("delete/{*releaseDate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        // DELETE a dvd based on the release date
        // /dvd/delete/2013/03/17
        public IHttpActionResult DeleteCity(DateTime releaseDate) {
            var result = dvdlist.SingleOrDefault(d => DateTime.Compare(d.ReleaseDate.Date, releaseDate.Date) == 0);
            if (releaseDate != null) {
                try {
                    if (ModelState.IsValid) {
                        dvdlist.Remove(result);
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
