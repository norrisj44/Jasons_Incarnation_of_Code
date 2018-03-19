using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAL
    {
        List<Weather> Get5DayWeather(string parkCode);
    }
}
