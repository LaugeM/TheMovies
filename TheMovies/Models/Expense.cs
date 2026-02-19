using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Models
{
    public class Expense : IValuable
    {
        private static int expIDCount = 1;

        public DateTime Date { get; set; }
        public double ExpAmount { get; set; }
        public string Note { get; set; }
        public int ID { get; }

        public Expense()
        {
            ID = expIDCount++;
        }

        public Expense(int id)
        {
            ID = id;
        }

        // Was supposed to be used for UC#3 & UC#4
        //
        //public double GetValue()
        //{
        //    return 0;
        //}

    }
}
