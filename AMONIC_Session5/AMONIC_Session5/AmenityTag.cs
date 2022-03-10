using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Session5
{
    public class AmenityTag
    {
        public Amenities Amenity { get; set; }
        public bool Payed { get; set; }

        public AmenityTag(Amenities amenity)
        {
            Amenity = amenity;
            Payed = false;
        }

        public AmenityTag()
        {

        }
    }
}
