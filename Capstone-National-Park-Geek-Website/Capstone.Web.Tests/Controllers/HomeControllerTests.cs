using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void HomeController_IndexAction_ReturnIndexView()
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);
            WeatherSqlDAL weatherDAL = new WeatherSqlDAL(ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString);

            //Arrange
            HomeController controller = new HomeController(parkDAL, weatherDAL);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}