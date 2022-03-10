using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AMONIC_Mobile
{
    public class Flight
    {
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("time")]
        public TimeSpan Time { get; set; }
        [JsonProperty("aircraft")]
        public string Aircraft { get; set; }
        public DateTime Date { get; set; }
    }
}
