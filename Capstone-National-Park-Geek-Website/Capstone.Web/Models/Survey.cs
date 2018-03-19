using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Capstone.Web.DAL;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class Survey
    {
        //public int SurveyId { get; set; }

        [Required]
        public string ParkCode { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-z\.]{2,6})$", ErrorMessage = "Email must be in form someone@example.com")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z]{1,2}|[a-zA-Z]+)$")]
        public string State { get; set; }

        [Required]
        public string ActivityLevel { get; set; }

        public int FavoriteCount { get; set; }

        public string ParkName { get; set; }

        public static List<SelectListItem> ParkCodeList
        {
            get
            {
                ParkSqlDAL parkDAL = new ParkSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
                List<Park> parks = parkDAL.GetParkList();

                List<SelectListItem> parkList = new List<SelectListItem>();

                foreach (Park item in parks)
                {
                    parkList.Add(new SelectListItem { Text = item.ParkName, Value = item.ParkCode });
                }

                return parkList;
            }
        }
    }
}
