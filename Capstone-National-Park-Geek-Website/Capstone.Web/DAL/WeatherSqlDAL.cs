using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherDAL
    {
        string connectionString;
        string SQL_GetWeather = "SELECT * FROM weather WHERE parkCode = @parkCode ORDER BY fiveDayForecastValue";

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> Get5DayWeather(string parkCode)
        {
            List<Weather> weatherList = new List<Weather>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetWeather, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Weather weather = new Weather();
                        weather.ParkCode = Convert.ToString(reader["parkCode"]);
                        weather.FiveDayForcastValue = Convert.ToInt32(reader["fiveDayForeCastValue"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);

                        weatherList.Add(weather);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return weatherList;
        }
    }
}