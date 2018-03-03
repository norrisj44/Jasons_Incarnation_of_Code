using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteTest
    {
        [TestMethod]
        public void SiteConstructorTest()
        {
            Site testObj = new Site();

            Assert.IsNotNull(testObj);
        } 

        [TestMethod]
        public void SitePropertiesTest()
        {
            Site testObj = new Site();
            testObj.SiteID = 12;
            testObj.CampgroundID = 5;
            testObj.SiteNumber = 45;
            testObj.MaxOccupancy = 66;
            testObj.Accessible = true;
            testObj.MaxRVLength = 99;
            testObj.Utilities = false;

            Assert.AreEqual(12, testObj.SiteID);
            Assert.AreEqual(5, testObj.CampgroundID);
            Assert.AreEqual(45, testObj.SiteNumber);
            Assert.AreEqual(66, testObj.MaxOccupancy);
            Assert.IsTrue(testObj.Accessible);
            Assert.AreEqual(99, testObj.MaxRVLength);
            Assert.IsFalse(testObj.Utilities);
        }
    }
}
