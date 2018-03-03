using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        //Properties
        public int ParkID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstablishDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        //Methods

        /// <summary>
        /// Override of ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result;
            result = $"{this.Name}\n";
            result += "Location:".PadRight(20) + this.Location + "\n";
            result += "Established:".PadRight(20) + this.EstablishDate.ToShortDateString() + "\n";
            result += "Area:".PadRight(20) + this.Area.ToString() + "\n";
            result += "Annual Visitors:".PadRight(20) + this.Visitors.ToString() + "\n\n";
            result += WordWrap(this.Description);
            return result;
        }

        public string WordWrap(string str)
        {
            string result = string.Empty;
            string[] strArray = str.Split(' ');
            Queue<string> strQueue = new Queue<string>();
            foreach (string s in strArray)
            {
                strQueue.Enqueue(s);
            }

            while (strQueue.Count > 0)
            {
                string line = string.Empty;
                while (line.Length < 60 && strQueue.Count > 0)
                {
                    line += strQueue.Dequeue() + " ";
                }
                result += line + "\n";
            }

            return result;
        }
    }
}
