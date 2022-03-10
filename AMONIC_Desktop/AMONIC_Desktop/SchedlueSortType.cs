using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Desktop
{
    public class SchedlueSortType
    {
        public static SchedlueSortType None { get; } = new SchedlueSortType("None", null);
        public static SchedlueSortType ByDateTime { get; } = new SchedlueSortType("Date-Time", delegate(Schedules a, Schedules b) { return (a.Date + a.Time).CompareTo(b.Date + b.Time); });
        public static SchedlueSortType ByConfirmed { get; } = new SchedlueSortType("Confirmed", delegate (Schedules a, Schedules b) { return b.Confirmed.CompareTo(a.Confirmed); });
        public static SchedlueSortType ByEconomyPrice { get; } = new SchedlueSortType("Economy Price", delegate (Schedules a, Schedules b) { return a.EconomyPrice.CompareTo(b.EconomyPrice); });
        public static List<SchedlueSortType> SortTypes { get; } = new List<SchedlueSortType>() { ByDateTime, ByConfirmed, ByEconomyPrice };
        public string Title { get; }
        public Comparison<Schedules> Comparison { get; }

        public SchedlueSortType(string title, Comparison<Schedules> comparison)
        {
            Title = title;
            Comparison = comparison;
        }
    }
}
