﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        List<Survey> GetFavoriteParks();
        bool NewSurvey(Survey newSurvey);
    }
}
