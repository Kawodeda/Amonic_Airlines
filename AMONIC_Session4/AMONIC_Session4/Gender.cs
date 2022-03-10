using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session4
{
    public class Gender
    {
        public static Gender Male { get; } = new Gender() { Name = "Male"};
        public static Gender Female { get; } = new Gender() { Name = "Female" };
        public static Gender AllGenders { get; } = new Gender() { Name = "All genders" };
        public string Name { get; set; }

        public static Gender Parse(string gender)
        {
            switch(gender)
            {
                case "M":
                    return Male;
                case "F":
                    return Female;
                default:
                    return Male;
            }
        }
    }
}
