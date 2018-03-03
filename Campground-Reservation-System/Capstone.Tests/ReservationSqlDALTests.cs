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
    public class ReservationSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private int parkID = 0;
        private int campgroundID = 0;
        private int siteID = 0;
        private int reservationID = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

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
                
                cmd = new SqlCommand("INSERT INTO reservation ([site_id],[name],[from_date],[to_date]) " +
                    "VALUES (@siteID, 'Steve Carmichael', '2018-04-21', '2018-04-28'); SELECT CAST(SCOPE_IDENTITY() as int)", conn);
                cmd.Parameters.AddWithValue("@siteID", siteID);
                reservationID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void ReservationByCampgroundIDTest()
        {
            ReservationDAL sqlDAL = new ReservationDAL(connectionString);
            List<Reservation> reservations = sqlDAL.GetReservationsByCampgroundID(campgroundID);

            Assert.IsNotNull(reservations);
            Assert.AreEqual(1, reservations.Count);
        }

        [TestMethod]
        public void ReservationByParkIDTest()
        {
            ReservationDAL sqlDAL = new ReservationDAL(connectionString);
            List<Reservation> reservations = sqlDAL.GetReservationsByParkID(parkID);

            Assert.IsNotNull(reservations);
            Assert.AreEqual(1, reservations.Count);
        }

        [TestMethod]
        public void CreateReservation()
        {
            ReservationDAL sqlDAL = new ReservationDAL(connectionString);
            DateTime fromDate = new DateTime(2018, 3, 1);
            DateTime toDate = new DateTime(2018, 3, 8);

            int newReservation = sqlDAL.CreateReservation(siteID, "John Fulton", fromDate, toDate);

            Assert.AreEqual(reservationID+1, newReservation);
        }
    }
}
