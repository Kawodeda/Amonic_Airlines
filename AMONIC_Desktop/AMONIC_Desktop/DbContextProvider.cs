using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Desktop
{
    public class DbContextProvider
    {
        public static gr602_chudedEntities Context { get; } = new gr602_chudedEntities();
    }
}
