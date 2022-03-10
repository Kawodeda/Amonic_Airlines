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
    public class TicketsController : ApiController
    {
        private Random rnd = new Random((int)DateTime.Now.Ticks);
        private CabinTypeDto[] cabinTypes = new CabinTypeDto[] 
        { 
            new CabinTypeDto() { Type = "Economy"},
            new CabinTypeDto() { Type = "Business"},
            new CabinTypeDto() { Type = "First Class"}
        };

        public CabinTypeDto GetCabinType(int ticketId)
        {
            //return new CabinTypeDto() { Type = DbContextProvider.Context.Tickets.Find(ticketId).CabinTypes.Name };
            return cabinTypes[rnd.Next(0, 3)];
        }
    }
}
