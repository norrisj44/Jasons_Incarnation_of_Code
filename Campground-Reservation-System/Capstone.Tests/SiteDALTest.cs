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
    public class SiteDALTest
    {
        private TransactionScope trans;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private int parkID = 0;
        private int campgroundID = 0;
        private int siteID = 0;
        //private int numCampgrounds = 0;

        [TestInitialize]
        public void Initialize()
        {
            trans = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();

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
            trans.Dispose();
        }

        [TestMethod]
        public void GetAllSitesCampgroundTest()
        {
            SiteDAL testObj = new SiteDAL(connectionString);
            List<Site> sites = testObj.GetAvailableSitesCampground(campgroundID, DateTime.Today, DateTime.Today);

            Assert.AreEqual(1, sites.Count);
        }

        [TestMethod]
        public void GetAllSitesParkTest()
        {
            SiteDAL testObj = new SiteDAL(connectionString);
            List<Site> sites = testObj.GetAvailableSitesPark(parkID, DateTime.Today, DateTime.Today);

            Assert.AreEqual(1, sites.Count);
        }
    }
}

