using System;
using System.Collections.Generic;
using System.Configuration;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {

            NationalParkCLI parkCLI = new NationalParkCLI();

            while (true)
            {
                parkCLI.RunCLI();
            }
        }
    }
}
