using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;


namespace Capstone.Tests
{
    [TestClass]
    public class CampgroudClassTests
    {
        [TestMethod]
        public void CampgroundOutputTest()
        {
            Campground cg = new Campground
            {
                CampgroundID = 1,
                ParkID = 11,
                Name = "Test",
                OpenFromMM = "2",
                OpenToMM = "11",
                DailyFee = "35.0"
            };

            Assert.AreEqual(1,cg.CampgroundID);
            Assert.AreEqual(11, cg.ParkID);
            Assert.AreEqual("Test", cg.Name);
            Assert.AreEqual("February", cg.OpenFromMM);
            Assert.AreEqual("November", cg.OpenToMM);
            Assert.AreEqual("$35.00", cg.DailyFee);
        }
    }
}
