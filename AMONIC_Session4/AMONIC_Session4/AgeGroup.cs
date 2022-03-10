using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session4
{
    public class AgeGroup
    {
        public static AgeGroup Age18to24 { get; } = new AgeGroup(18, 24);
        public static AgeGroup Age25to39 { get; } = new AgeGroup(25, 39);
        public static AgeGroup Age40to59 { get; } = new AgeGroup(40, 59);
        public static AgeGroup Age60 { get; } = new AgeGroup(60);
        public static AgeGroup AllAges { get; } = new AgeGroup(0, 0) { Name = "All ages"};
        public int LowerYear { get; }
        public int? UpperYear { get; }
        public string Name { get; private set; }

        public AgeGroup(int lowerYear, int upperYear)
        {
            LowerYear = lowerYear;
            UpperYear = upperYear;
            Name = $"{LowerYear}-{UpperYear}";
        }

        public AgeGroup(int lowerYear)
        {
            LowerYear = lowerYear;
            UpperYear = null;
            Name = $"{lowerYear}+";
        }

        public bool CheckAge(string age)
        {
            if (age == "")
                return false;

            return Convert.ToInt32(age) >= LowerYear && (UpperYear == null ? true : Convert.ToInt32(age) <= UpperYear);
        }
    }
}
