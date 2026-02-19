using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Models
{
    public interface IValuable
    {
        int ID { get; }
        // Was supposed to be used for UC#3 & UC#4
        //
        //double GetValue();
    }
}
