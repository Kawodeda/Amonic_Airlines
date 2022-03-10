using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amonic_API.Models;

namespace Amonic_API.Controllers
{
    public class DbContextProvider
    {
        public static gr602_chudedEntities Context { get; } = new gr602_chudedEntities();
    }
}