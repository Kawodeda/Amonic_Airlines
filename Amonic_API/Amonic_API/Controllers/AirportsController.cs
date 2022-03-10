using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Amonic_API.Models.Dto;

namespace Amonic_API.Controllers
{
    public class AirportsController : ApiController
    {
        AirportDto[] airports = new AirportDto[] 
        { 
            new AirportDto() {Id = 2, Name = "AUH"},
            new AirportDto() {Id = 3, Name = "CAI"},
            new AirportDto() {Id = 4, Name = "BAH"},
            new AirportDto() {Id = 5, Name = "ADE"},
            new AirportDto() {Id = 6, Name = "DOH"},
            new AirportDto() {Id = 7, Name = "RUH"},
        };
        public IEnumerable<AirportDto> GetAirports()
        {
            //return DbContextProvider.Context.Airports.ToList().Select(x => new AirportDto() { Id = x.ID, Name = x.IATACode});
            return airports;
        }
    }
}
