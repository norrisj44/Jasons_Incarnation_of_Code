using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationClassTest
    {
        [TestMethod]
        public void TestReservationClass()
        {
            Reservation rv = new Reservation
            {
                ReservationID = 1,
                SiteID = 11,
                Name = "Test",
                FromDate = DateTime.Today,
                ToDate = DateTime.Today,
                CreateDate = DateTime.Today
            };

            Assert.AreEqual(1, rv.ReservationID);
            Assert.AreEqual(11, rv.SiteID);
            Assert.AreEqual("Test", rv.Name);
            Assert.AreEqual(DateTime.Today, rv.FromDate);
            Assert.AreEqual(DateTime.Today, rv.ToDate);
            Assert.AreEqual(DateTime.Today, rv.CreateDate);
        }
    }
}
