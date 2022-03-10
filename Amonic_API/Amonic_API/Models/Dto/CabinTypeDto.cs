using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amonic_API.Models.Dto
{
    public class CabinTypeDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}