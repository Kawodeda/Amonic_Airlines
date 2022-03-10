using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session3
{
    public class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birtdate { get; set; }
        public string PassportNumber { get; set; }
        public int CountryID { get; set; }
        public string Phone { get; set; }
        public Countries Country { get; set; }
    }
}
