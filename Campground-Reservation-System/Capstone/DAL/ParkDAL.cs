using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;


namespace Capstone.DAL
{
   public class ParkDAL
    {
        //Private Data Members
        private string connectionString;
        private string SQL_GetParks = @"SELECT * FROM park;";
        private string SQL_GetParkInfo = @"SELECT * FROM park WHERE park_id = @parkID;";

        //Constructor
        public ParkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Methods

        /// <summary>
        /// Returns a dictionary with parkID keys, park name values of all parks in system
        /// </summary>
        /// <returns></returns>
        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParks, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park park = new Park();

                        park.ParkID = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.Visitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);

                        parks.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }

        /// <summary>
        /// Returns a Park given parkID
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public Park GetParkInfo(int parkID)
        {
            Park park = new Park();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParkInfo, connection);
                    cmd.Parameters.AddWithValue("@parkID", parkID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        park.ParkID = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.Visitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return park;
        }
    }
}
