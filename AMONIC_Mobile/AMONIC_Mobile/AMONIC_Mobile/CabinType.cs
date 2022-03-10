using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AMONIC_Mobile
{
    public class CabinType
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
