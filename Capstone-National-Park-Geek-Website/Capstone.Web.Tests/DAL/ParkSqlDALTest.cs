using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Collections.Generic;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class ParkSqlDALTest
    {
        private TransactionScope tran;

        private string connectionString = ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString;
        private int numParks = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand(@"SELECT COUNT(*) FROM park", conn);
                numParks = (int)cmd.ExecuteScalar();

                //Insert a Dummy Record for Park                
                cmd = new SqlCommand(@"INSERT INTO park ([parkCode], [parkName], [state], [acreage], [elevationInFeet], [milesOfTrail], [numberOfCampsites],
[climate], [yearFounded], [annualVisitorCount], [inspirationalQuote], [inspirationalQuoteSource], [parkDescription], [entryFee], [numberOfAnimalSpecies]) 
VALUES ('tnp', 'Test National Park', 'Ohio', '1', '1', '1', '1', 'Woodland', '2018', '1', 'test', 'coder', 'description', '1', '1');" , conn);
                cmd.ExecuteNonQuery();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
            //tran.Complete();
        }

        [TestMethod]
        public void GetAllParksTest()
        {
            ParkSqlDAL sqlDAL = new ParkSqlDAL(connectionString);
            List<Park> park = sqlDAL.GetAllParks();

            Assert.IsNotNull(park);
            Assert.AreEqual(numParks + 1, park.Count);
        }

        [TestMethod]
        public void GetParkDetailTest()
        {
            ParkSqlDAL sqlDAL = new ParkSqlDAL(connectionString);
            Park park = sqlDAL.GetParkDetail("tnp");

            Assert.IsNotNull(park);
            Assert.AreEqual("Test National Park", park.ParkName);
            Assert.AreEqual("Woodland", park.Climate);
            Assert.AreEqual("2018", park.YearFounded.ToString());
        }
    }
}
