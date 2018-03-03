using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using System.Configuration;

namespace Capstone.Models
{
   public class Site
    {
        //Properties
        public int SiteID { get; set; }
        public int CampgroundID { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public bool Accessible { get; set; }
        public int MaxRVLength { get; set; }
        public bool Utilities { get; set; }

        //Methods

        /// <summary>
        /// Override of ToString method.
        /// </summary>
        /// <returns>Properly formatted site information</returns>
        /// Method pulls database information for campground name and campground cost.
        public string ToString(int numDays)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            CampgroundDAL cgDAL = new CampgroundDAL(connectionString);
            Campground cg = cgDAL.GetCampgroundByID(this.CampgroundID);

            string result;
            result = cg.Name.PadRight(35);
            result += this.SiteNumber.ToString().PadRight(10);
            result += this.MaxOccupancy.ToString().PadRight(12);
            result += this.Accessible ? "YES".PadRight(15) : "NO".PadRight(15);

            if (this.MaxRVLength > 0)
            {
                result += this.MaxRVLength.ToString().PadRight(10);
            }
            else
            {
                result += "N/A".PadRight(10);
            }

            result += this.Utilities ? "YES".PadRight(11) : "N/A".PadRight(11);
            result += cg.CalculateFee(numDays);

            return result;
        }

       
    }
}
