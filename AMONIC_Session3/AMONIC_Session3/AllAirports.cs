using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session3
{
    public partial class Airports
    {
        public static Airports AllAirports { get; } = new Airports() { IATACode = "All Airports"};
    }
}
