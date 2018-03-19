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
    public class SurveySqlDALTests
    {
        private TransactionScope tran;

        private string connectionString = ConfigurationManager.ConnectionStrings["ParkWeather"].ConnectionString;
        private int surveyCount = 0;

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

                cmd = new SqlCommand(@"INSERT INTO park ([parkCode], [parkName], [state], [acreage], [elevationInFeet], [milesOfTrail], [numberOfCampsites],
[climate], [yearFounded], [annualVisitorCount], [inspirationalQuote], [inspirationalQuoteSource], [parkDescription], [entryFee], [numberOfAnimalSpecies]) 
VALUES ('NTP', 'Test National Park', 'Ohio', '1', '1', '1', '1', 'Woodland', '2018', '1', 'test', 'coder', 'description', '1', '1');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(@"INSERT INTO park ([parkCode], [parkName], [state], [acreage], [elevationInFeet], [milesOfTrail], [numberOfCampsites],
[climate], [yearFounded], [annualVisitorCount], [inspirationalQuote], [inspirationalQuoteSource], [parkDescription], [entryFee], [numberOfAnimalSpecies]) 
VALUES ('GNC', 'Test National Park', 'Ohio', '1', '1', '1', '1', 'Woodland', '2018', '1', 'test', 'coder', 'description', '1', '1');", conn);
                cmd.ExecuteNonQuery();

                //Insert a Dummy 5 day forecast               
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('NNN', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('NTP', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('NTP', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('GNC', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('GNC', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"INSERT INTO survey_result VALUES ('GNC', 'email', 'ohio', 'high')", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(@"SELECT Count(parkCode) FROM survey_result GROUP BY parkCode", conn);  //
                surveyCount = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
            //tran.Complete();
        }

        [TestMethod]
        public void GetFavoriteParksTest()
        {
            SurveySqlDAL sqlDAL = new SurveySqlDAL(connectionString);
            List<Survey> listOfFavoriteParks = sqlDAL.GetFavoriteParks();

            Assert.IsNotNull(listOfFavoriteParks);
            Assert.AreEqual(surveyCount + 3, listOfFavoriteParks.Count);
            Assert.AreEqual("GNC", listOfFavoriteParks[listOfFavoriteParks.FindIndex(list => list.ParkCode.Contains("GNC"))].ParkCode);
            Assert.AreEqual("NTP", listOfFavoriteParks[listOfFavoriteParks.FindIndex(list => list.ParkCode.Contains("NTP"))].ParkCode);
            Assert.AreEqual("NNN", listOfFavoriteParks[listOfFavoriteParks.FindIndex(list => list.ParkCode.Contains("NNN"))].ParkCode);
        }

        [TestMethod]
        public void NewSurveyTest()
        {
            SurveySqlDAL sqlDAL = new SurveySqlDAL(connectionString);
            Survey survey = new Survey
            {
                ParkCode = "GNC",
                Email = "email",
                State = "ohio",
                ActivityLevel = "high",
            };

            Assert.AreEqual(true, sqlDAL.NewSurvey(survey));
        }

    }
}
