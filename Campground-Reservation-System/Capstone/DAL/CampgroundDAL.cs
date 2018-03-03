using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connectionString;
        private string SQL_GetAllCampgrounds = "SELECT * FROM campground";

        public CampgroundDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> GetCampgrounds (int parkID)
        {
            string SQL_GetCampgroundsByParkID = SQL_GetAllCampgrounds + " WHERE park_id = @parkID";
            List<Campground> result = new List<Campground>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetCampgroundsByParkID, connection);
                    cmd.Parameters.AddWithValue("@parkID", parkID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground cg = new Campground();
                        cg.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        cg.ParkID = Convert.ToInt32(reader["park_id"]);
                        cg.Name = Convert.ToString(reader["name"]);
                        cg.OpenFromMM = Convert.ToString(reader["open_from_mm"]);
                        cg.OpenToMM = Convert.ToString(reader["open_to_mm"]);
                        cg.DailyFee = Convert.ToString(reader["daily_fee"]);

                        result.Add(cg);
                    }
                }
            }
            catch (SqlException ex)
            {

                throw;
            }

            return result;
        }

        public Campground GetCampgroundByID(int campgroundID)
        {
            Campground result = new Campground();
            string SQL_GetCampgroundByCgID = SQL_GetAllCampgrounds + " WHERE campground_id = @campgroundID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetCampgroundByCgID, connection);
                    cmd.Parameters.AddWithValue("@campgroundID", campgroundID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground cg = new Campground();
                        cg.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        cg.ParkID = Convert.ToInt32(reader["park_id"]);
                        cg.Name = Convert.ToString(reader["name"]);
                        cg.OpenFromMM = Convert.ToString(reader["open_from_mm"]);
                        cg.OpenToMM = Convert.ToString(reader["open_to_mm"]);
                        cg.DailyFee = Convert.ToString(reader["daily_fee"]);

                        result = cg;
                    }
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
