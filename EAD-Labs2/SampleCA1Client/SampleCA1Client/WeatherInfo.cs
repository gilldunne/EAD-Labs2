using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCA1Client.Models {
    class WeatherInfo {
        public String City { get; set; }
        public double Temperature { get; set; }
        public int WindSpeed { get; set; }
        public String WeatherConditions { get; set; }
        public bool WeatherWarnings { get; set; }
    }
}
