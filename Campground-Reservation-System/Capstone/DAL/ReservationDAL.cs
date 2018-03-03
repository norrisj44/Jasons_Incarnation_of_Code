using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString;
        private string SQL_GetAllReservations = "SELECT * FROM reservation JOIN site ON site.site_id = reservation.site_id";

        public ReservationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Returns a List of type Reservation for a given Site.CampgroundID or Campground.CampgroundID.
        /// </summary>
        /// <param name="campgroundID"></param>
        /// <returns></returns>
        public List<Reservation> GetReservationsByCampgroundID(int campgroundID)
        {
            string SQL_GetReservationsByCampgroundID = SQL_GetAllReservations + " WHERE site.campground_id = @campgroundID";
            List<Reservation> result = new List<Reservation>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetReservationsByCampgroundID, connection);
                    cmd.Parameters.AddWithValue("@campgroundID", campgroundID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation rv = new Reservation();
                        rv.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                        rv.SiteID = Convert.ToInt32(reader["site_id"]);
                        rv.Name = Convert.ToString(reader["name"]);
                        rv.FromDate = Convert.ToDateTime(reader["from_date"]);
                        rv.ToDate = Convert.ToDateTime(reader["to_date"]);
                        rv.CreateDate = Convert.ToDateTime(reader["create_date"]);

                        result.Add(rv);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Returns a List of type Reservation for a given Park.ParkID or Campground.ParkID. 
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public List<Reservation> GetReservationsByParkID(int parkID)
        {
            string SQL_GetReservationsByParkID = SQL_GetAllReservations + " JOIN campground ON campground.campground_id = site.campground_id WHERE campground.park_id = @parkID";
            List<Reservation> result = new List<Reservation>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetReservationsByParkID, connection);
                    cmd.Parameters.AddWithValue("@parkID", parkID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation rv = new Reservation();
                        rv.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                        rv.SiteID = Convert.ToInt32(reader["site_id"]);
                        rv.Name = Convert.ToString(reader["name"]);
                        rv.FromDate = Convert.ToDateTime(reader["from_date"]);
                        rv.ToDate = Convert.ToDateTime(reader["to_date"]);
                        rv.CreateDate = Convert.ToDateTime(reader["create_date"]);

                        result.Add(rv);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes a SQL INSERT INTO statement for given parameters on dbo NationalPark.Reservation.
        /// Returns the bool True when successful.
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="name"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public int CreateReservation(int siteID, string name, DateTime fromDate, DateTime toDate)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation ([site_id],[name],[from_date],[to_date]) " +
                    "VALUES (@siteID, @name, @fromDate, @toDate); SELECT CAST(SCOPE_IDENTITY() as int)", connection);
                    cmd.Parameters.AddWithValue("@siteID", siteID);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    result = (int)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return result;
        }
    }
}
