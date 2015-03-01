using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRentalClient {
    class DVD {
        public String Title { get; set; }
        public double RunningTime { get; set; }
        public int AgeRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        private DateTime rentalDate;
        public DateTime RentalDate { get { return rentalDate; } set { rentalDate = DateTime.Now; } }
   }
}
