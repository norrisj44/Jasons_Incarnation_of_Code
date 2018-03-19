using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.DAL;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int Elevation { get; set; }
        public double MilesOfTrail { get; set; }
        public int NumberOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string QuoteSource { get; set; }
        public string ParkDescription { get; set; }
        public double EntryFee { get; set; }
        public int NumberOfAnimalSpecies { get; set; }
        public int TempValue { get; set ; }

        public double CelsiusConversion(int tempInF)
        {
            return Math.Round(((tempInF - 32) / 1.8),0);
        }

    }
}