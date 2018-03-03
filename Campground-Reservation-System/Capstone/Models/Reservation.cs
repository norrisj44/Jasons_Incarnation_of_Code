using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        private int reservationID;
        private int siteID;
        private string name;
        private DateTime fromDate;
        private DateTime toDate;
        private DateTime createDate;

        public int ReservationID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return this.Name.PadRight(40) + this.FromDate.ToShortDateString().PadRight(15) + this.ToDate.ToShortDateString().PadRight(15) + this.CreateDate.ToShortDateString();
        }
    }
}
