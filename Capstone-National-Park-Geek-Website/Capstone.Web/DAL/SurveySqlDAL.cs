using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveyDAL
    {
        string connectionString;
        string SQL_GetFavoriteParks = @"SELECT COUNT(survey_result.parkCode) as topParks,survey_result.parkCode, park.parkName FROM survey_result JOIN park ON park.parkCode = survey_result.parkCode GROUP BY survey_result.parkCode, park.parkName ORDER BY topParks desc";

        string SQL_NewSurvey = "INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel)";

        public SurveySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Survey> GetFavoriteParks()
        {
            List<Survey> surveyList = new List<Survey>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetFavoriteParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Survey survey = new Survey
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),                           
                            FavoriteCount = Convert.ToInt32(reader["topParks"])
                        };

                        surveyList.Add(survey);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return surveyList;
        }

        public bool NewSurvey(Survey newSurvey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_NewSurvey, conn);
                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", newSurvey.Email);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0); //true if one row was affected
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}


