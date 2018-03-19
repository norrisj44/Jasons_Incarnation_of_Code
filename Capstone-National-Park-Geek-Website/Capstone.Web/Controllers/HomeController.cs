using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAL parkDAL;
        private IWeatherDAL weatherDAL;

        //Use NinjectWebCommon 3.2.0 to initialize dbo connection squirrelReviews
        public HomeController(IParkDAL parkDAL, IWeatherDAL weatherDAL)
        {
            this.parkDAL = parkDAL;
            this.weatherDAL = weatherDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index", parkDAL.GetAllParks());
        }

        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                id = "CVNP";
            }
            Session["Weather"] = weatherDAL.Get5DayWeather(id);

            var degrees = Session["degrees"];
            if (degrees == null)
            {
                degrees = 0;
            }
            Session["degrees"] = degrees;
            Park park = parkDAL.GetParkDetail(id);
            park.TempValue = (int)degrees;

            return View("Detail", park);
        }

        [HttpPost]
        public ActionResult Detail(Park updatedPark)
        {
            int degrees = updatedPark.TempValue;
            Session["degrees"] = degrees;

            return View("Detail", updatedPark);
        }
    }
}