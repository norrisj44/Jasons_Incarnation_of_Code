using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        ISurveyDAL surveyDAL;

        public SurveyController(ISurveyDAL surveyDAL)
        {
            this.surveyDAL = surveyDAL;
        }
        
        // GET: Survey
        public ActionResult NewSurvey()
        {
            return View("NewSurvey");
        }

        [HttpPost]
        public ActionResult NewSurvey(Survey newSurvey)
        {
            if (!ModelState.IsValid)
            {
                return View("NewSurvey", newSurvey);
            }
            surveyDAL.NewSurvey(newSurvey);

            return RedirectToAction("Favorites");
        }

        public ActionResult Favorites()
        {
            return View("Favorites", surveyDAL.GetFavoriteParks());
        }
    }
}