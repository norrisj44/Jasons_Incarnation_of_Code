using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers.Tests
{
    [TestClass]
    public class SurveyControllerTests
    {
        [TestMethod()]
        public void SurveyController_NewSurveyAction_ReturnNewSurveyView()
        {
            //ParkSqlDAL parkDAL = new ParkSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
            //WeatherSqlDAL weatherDAL = new WeatherSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
            SurveySqlDAL surveyDAL = new SurveySqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);

            //Arrange
            SurveyController controller = new SurveyController(surveyDAL);

            //Act
            ViewResult result = controller.NewSurvey() as ViewResult;

            //Assert
            Assert.AreEqual("NewSurvey", result.ViewName);
        }

        [TestMethod()]
        public void SurveyController_FavoritesAction_ReturnFavoritesView()
        {
            //ParkSqlDAL parkDAL = new ParkSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
            //WeatherSqlDAL weatherDAL = new WeatherSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
            SurveySqlDAL surveyDAL = new SurveySqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);

            //Arrange
            SurveyController controller = new SurveyController(surveyDAL);

            //Act
            ViewResult result = controller.Favorites() as ViewResult;

            //Assert
            Assert.AreEqual("Favorites", result.ViewName);
        }
    }
}
