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
    public class WeatherSqlDALTests
    {
        private TransactionScope tran;

        private string connectionString = ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString;
        private int numWeather = 0;

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
                cmd = new SqlCommand(@"INSERT INTO park ([parkCode], [parkName], [state], [acreage], [elevationInFeet], [milesOfTrail], [numberOfCampsites],
[climate], [yearFounded], [annualVisitorCount], [inspirationalQuote], [inspirationalQuoteSource], [parkDescription], [entryFee], [numberOfAnimalSpecies]) 
VALUES ('NNN', 'Test National Park', 'Ohio', '1', '1', '1', '1', 'Woodland', '2018', '1', 'test', 'coder', 'description', '1', '1');", conn);
                cmd.ExecuteNonQuery();

                //Insert a Dummy 5 day forecast               
                cmd = new SqlCommand(@"INSERT INTO weather VALUES ('NNN', 1, 38,62,'rain')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO weather VALUES ('NNN', 2, 38,56,'partly cloudy')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO weather VALUES ('NNN', 3, 51,66,'partly coudy')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO weather VALUES ('NNN', 4, 55,65,'rain')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO weather VALUES ('NNN', 5, 53,69,'thunderstorms')", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(@"SELECT Count(*) FROM weather WHERE parkCode = 'NNN'", conn);
                numWeather = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
            //tran.Complete();
        }

        [TestMethod]
        public void Get5DayWeatherTest()
        {
            WeatherSqlDAL sqlDAL = new WeatherSqlDAL(connectionString);
            List<Weather> ListOfParkWeather = sqlDAL.Get5DayWeather("NNN");

            Assert.IsNotNull(ListOfParkWeather);
            Assert.AreEqual(numWeather, ListOfParkWeather.Count);
            Assert.AreEqual("NNN", ListOfParkWeather[0].ParkCode);
            Assert.AreEqual(1, ListOfParkWeather[0].FiveDayForcastValue);
            Assert.AreEqual(5, ListOfParkWeather[4].FiveDayForcastValue);
        }

        [TestMethod]
        public void CelsiusConvertersionTest()
        {
            Park testWeather = new Park();
            Assert.AreEqual(20, Math.Round(testWeather.CelsiusConversion(68),2));
            Assert.AreEqual(-12, Math.Round(testWeather.CelsiusConversion(10),2));
            Assert.AreEqual(10, Math.Round(testWeather.CelsiusConversion(50),2));
            Assert.AreEqual(260, Math.Round(testWeather.CelsiusConversion(500),2));
            Assert.AreEqual(4, Math.Round(testWeather.CelsiusConversion(40),2));
        }
    }
}
