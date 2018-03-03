using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        private int campgroundID;
        private int parkID;
        private string name;
        private int openFromMM; //int describing month from 1-12 that they open up (first of month)
        private int openToMM; //int describing month from 1-12 that they shut down (End of Month) 
        private decimal dailyFee = 0.0M;

        public Campground()
        {

        }

        public int CampgroundID { get; set; }
        public int ParkID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            string result;
            result = this.Name.PadRight(40);
            result += this.OpenFromMM.PadRight(12);
            result += this.OpenToMM.PadRight(12);
            result += this.DailyFee;

            return result;
        }

        /// <summary>
        /// Converts integer month(1-12) from database to a string month (January-December).
        /// When setting property, use string "integer": OpenFromMM = "2".
        /// </summary>
        public string OpenFromMM { get { return ConvertToMonth(openFromMM).ToString(); } set { openFromMM = int.Parse(value); } }

        /// <summary>
        /// Converts integer month(1-12) from database to a string month (January-December).
        /// When setting property, use string "integer": Campground.OpenFromMM = "2".
        /// </summary>
        public string OpenToMM { get { return ConvertToMonth(openToMM); } set { openToMM = int.Parse(value); } }

        /// <summary>
        /// Get converts decimal from database to string in Currency Format.
        /// When setting property, use string "decimal": Campground.DailyFee = "35.5".
        /// </summary>
        public string DailyFee { get { return dailyFee.ToString("C"); } set { dailyFee = decimal.Parse(value); } }

        private string ConvertToMonth(int openFromMM)
        {
            string result = "";
            switch (openFromMM)
            {
                case 1: result = "January";     break;
                case 2: result = "February";    break;
                case 3: result = "March";       break;
                case 4: result = "April";       break;
                case 5: result = "May";         break;
                case 6: result = "June";        break;
                case 7: result =  "July";       break;
                case 8: result =  "August";     break;
                case 9: result =  "September";  break;
                case 10: result =  "October";   break;
                case 11: result =  "November";  break;
                case 12: result =  "December";  break;
            }
            return result;
        }

        public string CalculateFee(int numDays)
        {
            return (numDays * dailyFee).ToString("c");
        }
    }
}
