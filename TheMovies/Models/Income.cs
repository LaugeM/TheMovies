using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheMovies.Models
{
    public class Income : IValuable
    {
        private static int incIDCount = 1;

        public string Initials { get; set; }
        public DateTime Date { get; set; }
        public double IncAmount { get; set; }
        public string Service { get; set; }
        public bool PaymentStatus { get; set; } = false;
        public int ID { get; }

        public Income()
        {
            ID = incIDCount++;
        }

        public Income(int id)
        {
            ID = id;
        }

        // Was supposed to be used for UC#3 & UC#4
        //
        //public double GetValue()
        //{
        //    return IncAmount;
        //}
    }
}