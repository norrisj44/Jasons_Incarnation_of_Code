using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL: IParkDAL
    {
        private string SQL_GetAllParks = "SELECT parkCode, parkName, state, parkDescription FROM park";
        private string SQL_GetParkDetail = "SELECT * FROM park WHERE parkCode = @parkCode";
        private string SQL_GetParkList = "SELECT parkCode, parkName FROM park";

        string connectionString;

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> result = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"])
                        };
                        result.Add(p);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }

        public Park GetParkDetail(string parkCode)
        {
            Park result = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkDetail, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            Elevation = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            EntryFee = Convert.ToInt32(reader["entryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
                        };

                        result = p;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }

        public List<Park> GetParkList()
        {
            List<Park> result = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkList, conn);
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Park p = new Park
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                        };

                        result.Add(p);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}