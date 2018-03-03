using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class NationalParkCLI
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        const string Command_Quit = "q";

        public void RunCLI()
        {
            ParkDAL parkDAL = new ParkDAL(connectionString);
            List<Park> parks = parkDAL.GetParks();

            Console.Clear();
            Console.WriteLine("Select a Park for Further Details");
            int i = 1;  //menu index
            foreach (Park park in parks)
            {
                Console.WriteLine(i.ToString() + ")".PadRight(4) + park.Name);
                i++;
            }

            Console.WriteLine("Q)".PadRight(4) + " Quit");

            string input = CLIHelper.GetString("Please enter a selection: ");

            if (input.ToLower().Equals("q"))
            {
                Environment.Exit(1);
            }
            else if (int.Parse(input) > 0 && int.Parse(input) < i)
            {
                ViewParkInfo(parks[int.Parse(input) - 1].ParkID);
            }
            else
            {
                Console.WriteLine("Make a valid selection");
            }
            return;
        }

        public void ViewParkInfo(int parkID)
        {
            ParkDAL park = new ParkDAL(connectionString);
            Park curPark = park.GetParkInfo(parkID);

            Console.Clear();
            Console.WriteLine(curPark);
            bool invalidSelection = false;

            do
            {
                Console.WriteLine("Select a Command");
                Console.WriteLine(" 1) View Campgrounds");
                Console.WriteLine(" 2) View Reservations within 30 days");
                Console.WriteLine(" 3) Search for Reservation");
                Console.WriteLine(" 4) Return to Park Selection Screen");

                string input = CLIHelper.GetString("Please enter a selection: ");

                switch (input)
                {
                    case "1":
                        ViewCampgrounds(parkID);
                        break;
                    case "2":
                        ViewParkReservations(parkID);
                        break;
                    case "3":
                        MakeReservation(parkID, 0);
                        break;
                    case "4":
                        invalidSelection = false;
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection.");
                        invalidSelection = true;
                        break;
                }
            } while (invalidSelection);

            return;
        }

        public void ViewCampgrounds(int parkID)
        {
            CampgroundDAL campground = new CampgroundDAL(connectionString);
            List<Campground> campgrounds = campground.GetCampgrounds(parkID);

            Console.Clear();
            Console.WriteLine("   Name".PadRight(43) + "Open".PadRight(12) + "Close".PadRight(12) + "Daily Fee");

            int index = 1;
            foreach (Campground camp in campgrounds)
            {
                Console.Write(index + ") ");
                Console.WriteLine(camp);
                index++;
            }

            bool invalidSelection = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Select a command");
                Console.WriteLine("1) Search for Available Reservation");
                Console.WriteLine("2) Return to Park Information Screen");
                string input = CLIHelper.GetString("Please enter a selection: ");

                switch (input)
                {
                    case "1":
                        int campInput = CLIHelper.GetInteger("Which campground do you wish to make a reservation for: ");
                        MakeReservation(parkID, campgrounds[campInput - 1].CampgroundID);
                        return;
                    case "2":
                        ViewParkInfo(parkID);
                        break;
                    default:
                        Console.WriteLine("Please make a valid selection: ");
                        invalidSelection = true;
                        break;
                }
            } while (invalidSelection);
        }

        public void ViewParkReservations(int parkID)
        {
            ReservationDAL reservation = new ReservationDAL(connectionString);
            List<Reservation> reservations = reservation.GetReservationsByParkID(parkID);

            Console.WriteLine("\nReserved For".PadRight(40) + "Start Date".PadRight(15) + "End Date".PadRight(15) + "Created");
            foreach (Reservation res in reservations)
            {
                if (res.FromDate.CompareTo(DateTime.Today) > 0)
                {
                    if (res.ToDate.CompareTo(DateTime.Today.AddDays(30)) < 0)
                    {
                        Console.WriteLine(res);
                    }
                }
            }
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
            ViewParkInfo(parkID);
        }

        public void MakeReservation(int parkID, int campgroundID)
        {
            bool searchAgain = false;
            SiteDAL siteDAL = new SiteDAL(connectionString);
            List<Site> availableSites = new List<Site>();
            DateTime fromDate;
            DateTime toDate;

            do
            {
                searchAgain = false;
                fromDate = CLIHelper.GetDateTime("What is the arrival date: ");
                int numberOfAttempts = 0;
                do
                {
                    if (numberOfAttempts > 0)
                    {
                        Console.WriteLine("Departure date cannot be before/equal to arrival date.");
                    }
                    toDate = CLIHelper.GetDateTime("What is the departure date: ");
                    numberOfAttempts++;
                } while (toDate.CompareTo(fromDate) <= 0);

                Console.WriteLine();
                Console.WriteLine("".PadRight(4) + "Campground".PadRight(35) + "Site No.".PadRight(10) + "Max Occup.".PadRight(12) + "Accessible?".PadRight(15) + "RV Len".PadRight(10) + "Utilities".PadRight(11) + "Cost");

                if (campgroundID == 0)
                {
                    availableSites = siteDAL.GetAvailableSitesPark(parkID, toDate, fromDate);
                }
                else
                {
                    availableSites = siteDAL.GetAvailableSitesCampground(campgroundID, toDate, fromDate);
                }

                if (availableSites.Count == 0)
                {
                    Console.WriteLine("There are no available sites.");
                    string askSearchAgain = CLIHelper.GetString("Do you wish to search again? ");

                    switch (askSearchAgain.ToLower())
                    {
                        case "y":
                            searchAgain = true;
                            break;
                        default:
                            Console.WriteLine("Thank you, you will be returned to the Park Selection screen.  Press enter to continue...");
                            Console.ReadLine();
                            return;
                    }
                }
            } while (searchAgain);

            TimeSpan interval = toDate.Date.Subtract(fromDate);
            int numDays = 1 + interval.Days;
            int index = 1;

            foreach (Site s in availableSites)
            {
                Console.WriteLine(index.ToString().PadRight(4) + s.ToString(numDays));
                index++;
            };

            int siteToBook = CLIHelper.GetInteger("Which site should be reserved (enter 0 to cancel): ");
            bool invalidSelection = false;
            do
            {

                if (siteToBook == 0)
                {
                    invalidSelection = false;
                    Console.WriteLine("Thank you, you will be returned to the Park Selection screen.  Press enter to continue...");
                    Console.ReadLine();
                }
                else if (siteToBook > 0 && siteToBook < index)
                {
                    ReservationDAL reservationDAL = new ReservationDAL(connectionString);
                    string nameToBook = CLIHelper.GetString("What name should the reservation be made under? ");
                    int confirm = reservationDAL.CreateReservation(availableSites[siteToBook - 1].SiteID, nameToBook, fromDate, toDate);
                    Console.WriteLine($"\nThe reservation has been made and the confirmation id is {confirm}.");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    invalidSelection = false;
                }
                else
                {
                    siteToBook = CLIHelper.GetInteger("Please enter a valid selection: ");
                    invalidSelection = true;
                }
            } while (invalidSelection);
            return;
        }
    }
}
