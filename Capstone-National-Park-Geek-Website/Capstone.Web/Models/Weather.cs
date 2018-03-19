using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForcastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }


        public string WeatherRecommendation(string forecast, int high, int low)
        {
            string recommendation = "";
            switch (forecast)
            {
                case "rain":
                    recommendation = "Pack rain gear and wear waterproof shoes!";
                    break;
                case "snow":
                    recommendation = "Pack snowshoes!";
                    break;
                case "thunderstorms":
                    recommendation = "Seek shelter and avoid hiking on exposed ridges!";
                    break;
                case "sunny":
                    recommendation = "Pack sunblock!";
                    break;
            }

            if (high > 75)
            {
                recommendation += " Bring extra gallon of water.";
            }
            if (high - low > 20)
            {
                recommendation += " Wear breathable layers.";
            }
            if (high <20 || low <20)
            {
                recommendation += " Beware the dangers of exposure to frigid temperatures.";
            }

            return recommendation;
        }
    }
}