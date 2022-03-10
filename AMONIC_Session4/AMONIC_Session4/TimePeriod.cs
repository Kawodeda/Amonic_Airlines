using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session4
{
    public class TimePeriod
    {
        public static TimePeriod AllSurveys { get; } = new TimePeriod() { Name = "All surveys"};
        public string Name { get; private set; }
        public DateTime Date { get; set; }

        public TimePeriod(DateTime date)
        {
            Date = date;
            Name = date.ToString("Y", new CultureInfo("en-US"));
        }

        public TimePeriod()
        {

        }
    }
}
