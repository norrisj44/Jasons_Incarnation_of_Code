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
    public class ParkDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private int parkID = 0;
        private int campgroundID = 0;
        private int siteID = 0;
        private int numCampgrounds = 0;
        private int numParks = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM campground", conn);
                numCampgrounds = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM park", conn);
                numParks = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO park ([name],[location],[establish_date],[area],[visitors],[description]) " +
                    "VALUES ('ABC Park', 'ABC Country', '2018-01-01', 1, 5, 'This is a test park'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                parkID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO campground ([park_id],[name],[open_from_mm],[open_to_mm],[daily_fee]) " +
                    "VALUES (@parkID, 'ABC Camp', 1, 12, 35.5); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                cmd.Parameters.AddWithValue("@parkID", parkID);
                campgroundID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO site ([campground_id],[site_number],[max_occupancy],[accessible],[max_rv_length],[utilities]) " +
                    "VALUES (@campgroundID, 13, 6, 0, 10, 0); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                cmd.Parameters.AddWithValue("@campgroundID", campgroundID);
                siteID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetParksTest()
        {
            ParkDAL testObj = new ParkDAL(connectionString);
            List<Park> parks = testObj.GetParks();

            Assert.AreEqual(numParks + 1, parks.Count);
        }

        [TestMethod]
        public void GetParkInfo()
        {
            ParkDAL testObj = new ParkDAL(connectionString);
            Park park = testObj.GetParkInfo(parkID);

            Assert.AreEqual("ABC Park", park.Name);
            Assert.AreEqual("ABC Country", park.Location);
            Assert.AreEqual(new DateTime(2018, 01, 01), park.EstablishDate);
            Assert.AreEqual(1, park.Area);
            Assert.AreEqual(5, park.Visitors);
            Assert.AreEqual("This is a test park", park.Description);
        }
    }
}
