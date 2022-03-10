using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Amonic_API.Models;
using Amonic_API.Models.Dto;

namespace Amonic_API.Controllers
{
    public class SchedulesController : ApiController
    {
        public IEnumerable<ScheduleDto> GetSchedules(string from, string to, DateTime date)
        {
            //return DbContextProvider.Context.Schedules.ToList().FindAll(
            //    x => x.Routes.Airports.IATACode == from &&
            //    x.Routes.Airports1.IATACode == to &&
            //    x.Date == date).Select(x => new ScheduleDto() { FlightNumber = x.FlightNumber, Price = (int)x.EconomyPrice, Time = x.Time, Aircraft = x.Aircrafts.MakeModel});

            return new ScheduleDto[] 
            {
                new ScheduleDto() { FlightNumber = "49", Price = 600, Time = new TimeSpan(17, 0, 0), Aircraft = "Boeing 738"},
                new ScheduleDto() { FlightNumber = "50", Price = 714, Time = new TimeSpan(21, 30, 0), Aircraft = "Boeing 739"}
            };
        }
    }
}
