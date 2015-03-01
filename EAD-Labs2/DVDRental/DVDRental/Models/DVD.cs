using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVDRental.Models {
    public class DVD {
        [Required(ErrorMessage="Title must be provided")]
        public String Title { get; set; }

        [Range(60, 180, ErrorMessage="Running Time out of bounds")]
        public double RunningTime { get; set; }

        [Range(0, 18, ErrorMessage="out of bounds")]
        public int AgeRating { get; set; }

        public DateTime ReleaseDate { get; set; }

        private DateTime rentalDate;
        public DateTime RentalDate { get { return rentalDate; } set { rentalDate = DateTime.Now; } }
    }
}