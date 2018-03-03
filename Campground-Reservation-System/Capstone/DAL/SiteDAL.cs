using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SiteDAL
    {
        //Private Data Members
        private string connectionString;
        private string SQL_GetAvailableSitesCampground = @" SELECT TOP 5 * FROM site WHERE campground_id = @campgroundID AND site.site_id NOT IN ( SELECT site.site_id FROM site JOIN reservation ON site.site_id = reservation.site_id WHERE reservation.from_date BETWEEN @fromDate AND @toDate OR reservation.to_date BETWEEN @fromDate AND @toDate OR (reservation.from_date<@fromDate AND reservation.to_date> @toDate))";
        private string SQL_GetAvailableSitesPark = @"SELECT TOP 5 * FROM site JOIN campground ON site.campground_id = campground.campground_id WHERE campground.park_id = @parkid AND site.site_id NOT IN ( SELECT site.site_id FROM site JOIN reservation ON site.site_id = reservation.site_id WHERE reservation.from_date BETWEEN @fromDate AND @toDate OR reservation.to_date BETWEEN @fromDate AND @toDate OR (reservation.from_date < @fromDate AND reservation.to_date > @toDate))";

        //Constructors
        public SiteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Methods

        /// <summary>
        /// Returns a List of Sites in a campground given campgroundID
        /// </summary>
        /// <param name="campgroundID">The id number for the desired campground</param>
        /// <returns>List(Site) containing all sites in given campground</returns>
        public List<Site> GetAvailableSitesCampground(int campgroundID, DateTime toDate, DateTime fromDate)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableSitesCampground, connection);
                    cmd.Parameters.AddWithValue("@campgroundID", campgroundID);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.SiteID = Convert.ToInt32(reader["site_id"]);
                        s.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return sites;
        }

        /// <summary>
        /// Returns a List of Sites in a park given parkID
        /// </summary>
        /// <param name="parkID">The id of the desired park</param>
        /// <returns>List(Site) of all sites in given park</returns>
        public List<Site> GetAvailableSitesPark(int parkID, DateTime toDate, DateTime fromDate)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableSitesPark, connection);
                    cmd.Parameters.AddWithValue("@parkID", parkID);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.SiteID = Convert.ToInt32(reader["site_id"]);
                        s.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(s);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return sites;
        }
    }
}
