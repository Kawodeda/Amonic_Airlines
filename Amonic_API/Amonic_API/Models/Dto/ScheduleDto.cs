using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Amonic_API.Models.Dto
{
    public class ScheduleDto
    {
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("time")]
        public TimeSpan Time { get; set; }
        [JsonProperty("aircraft")]
        public string Aircraft { get; set; }
    }
}