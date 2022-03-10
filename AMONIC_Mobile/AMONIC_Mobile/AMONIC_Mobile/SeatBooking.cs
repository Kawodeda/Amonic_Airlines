using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AMONIC_Mobile
{
    public class SeatBooking
    {
        [JsonProperty("row")]
        public string Row { get; set; }
        [JsonProperty("seat")]
        public string Seat { get; set; }
    }
}
