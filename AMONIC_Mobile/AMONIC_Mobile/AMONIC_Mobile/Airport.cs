using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AMONIC_Mobile
{
    public class Airport
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
