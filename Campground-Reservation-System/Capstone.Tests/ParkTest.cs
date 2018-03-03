using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using System.Collections.Generic;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkTest
    {
        [TestMethod]
        public void ParkConstructorTest()
        {
            Park testObj = new Park();

            Assert.IsNotNull(testObj);
        }

        [TestMethod]
        public void ParkPropertiesTest()
        {
            Park testObj = new Park
            {
                ParkID = 12,
                Name = "National Reserve",
                Location = "Ohio",
                EstablishDate = new DateTime(1991, 06, 06),
                Area = 1,
                Visitors = 1,
                Description = "Description Here"
            };

            Assert.AreEqual(12, testObj.ParkID);
            Assert.AreEqual("National Reserve", testObj.Name);
            Assert.AreEqual("Ohio", testObj.Location);
            Assert.AreEqual(new DateTime(1991, 06, 06), testObj.EstablishDate);
            Assert.AreEqual(1, testObj.Area);
            Assert.AreEqual(1, testObj.Visitors);
            Assert.AreEqual("Description Here", testObj.Description);
        }
    }
}
