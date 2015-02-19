using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleCA1.Models {
    public class WeatherInfo {
        [Required(ErrorMessage = "Invalid City")]
        public String City { get; set; }

        [Range(-50, 50, ErrorMessage = "Temp out of range")]
        public double Temperature { get; set; }

        [Range(0, 200, ErrorMessage = "Speed out of range")]
        public int WindSpeed { get; set; }

        [Required(ErrorMessage = "Invalid Condition")]
        public String WeatherConditions { get; set; }

        public bool WeatherWarnings { get; set; }
    }
}