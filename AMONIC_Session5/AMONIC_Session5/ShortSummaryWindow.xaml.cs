using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AMONIC_Session5
{
    /// <summary>
    /// Логика взаимодействия для ShortSummaryWindow.xaml
    /// </summary>
    public partial class ShortSummaryWindow : Window
    {
        private DateTime today = new DateTime(2017, 10, 29);
        private TimeSpan summaryTime = TimeSpan.FromDays(30);

        public ShortSummaryWindow()
        {
            InitializeComponent();
        }

        private void GenerateSummary()
        {
            var schedules = DBContextProvider.Context.Schedules.ToList().FindAll(x => x.Date + x.Time > today - summaryTime);

            int confirmedNum = schedules.FindAll(x => x.Confirmed).Count;
            int cancelledNum = schedules.FindAll(x => !x.Confirmed).Count;
            float averageFlightTime = (float)schedules.Sum(x => x.Routes.FlightTime) / summaryTime.Days * 60;

            flights_tb.Text = $"Number confirmed:   {confirmedNum}\n\nNumber cancelled:   {cancelledNum}\n\nAverage daily flight time:   {averageFlightTime} minutes";

            DateTime busiestDay = schedules.First().Date;
            DateTime mostQuietDay = schedules.First().Date;
            int busiestDayFlying = int.MinValue;
            int mostQuietDayFlying = int.MaxValue;

            var dates = schedules.Select(x => x.Date).Distinct().ToList();

            foreach(var date in dates)
            {
                var flights = schedules.FindAll(x => x.Date == date);
                int passengersCount = flights.Sum(x => x.Tickets.ToList().FindAll(y => y.Confirmed).Count);

                if(passengersCount > busiestDayFlying)
                {
                    busiestDay = date;
                    busiestDayFlying = passengersCount;
                }
                if(passengersCount < mostQuietDayFlying)
                {
                    mostQuietDay = date;
                    mostQuietDayFlying = passengersCount;
                }
            }

            passengers_tb.Text = $"\nBusiest day:   {busiestDay.ToString("dd/MM")} with {busiestDayFlying} flying\n\nMost quiet day:   {mostQuietDay.ToString("dd/MM")} with {mostQuietDayFlying} flying";

            var customesrs = schedules.SelectMany(x => x.Tickets).Select(x => $"{x.Firstname} {x.Lastname}").Distinct().ToList();
            List<string> topThree = new List<string>();
            List<int> topThreePurchases = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                string top = customesrs.First();
                int topPurchases = schedules.SelectMany(x => x.Tickets).ToList().FindAll(x => x.Firstname == top.Split(' ')[0] && x.Lastname == top.Split(' ')[1]).Count;

                foreach (string customer in customesrs)
                {
                    if (!topThree.Contains(customer))
                    {
                        int purchases = schedules.SelectMany(x => x.Tickets).ToList().FindAll(x => x.Firstname == customer.Split(' ')[0] && x.Lastname == customer.Split(' ')[1]).Count;
                        if (purchases > topPurchases)
                        {
                            top = customer;
                            topPurchases = purchases;
                        }
                    }
                }

                topThree.Add(top);
                topThreePurchases.Add(topPurchases);
            }

            try
            {
                top_customers_tb.Text = $"1. {topThree[0]} ({topThreePurchases[0]} Tickets)\n\n2. {topThree[1]} ({topThreePurchases[1]} Tickets)\n\n3. {topThree[2]} ({topThreePurchases[2]} Tickets)";
            }
            catch
            {

            }

            var offices = schedules.SelectMany(x => x.Tickets).Select(x => x.Users.Offices).Distinct().ToList();
            offices.Sort(delegate(Offices a, Offices b) {
                int aPurchases = schedules.SelectMany(x => x.Tickets).ToList().FindAll(x => x.Users.Offices == a).Sum(x => GetTicketPrice(x));
                int bPurchases = schedules.SelectMany(x => x.Tickets).ToList().FindAll(x => x.Users.Offices == b).Sum(x => GetTicketPrice(x));
                if (aPurchases > bPurchases)
                    return -1;
                else if (aPurchases < bPurchases)
                    return 1;
                else
                    return 0;
            });

            for(int i = 0; i < (3 < offices.Count ? 3 : offices.Count); i++)
            {
                top_offices_tb.Text += $"{i + 1}. {offices[i].Title}";
                if (i < (3 < offices.Count ? 3 : offices.Count) - 1)
                    top_offices_tb.Text += "\n\n";
            }

            var yeasterdayTickets = schedules.FindAll(x => x.Date == today - TimeSpan.FromDays(1)).SelectMany(x => x.Tickets).ToList();
            var twoDaysAgoTickets = schedules.FindAll(x => x.Date == today - TimeSpan.FromDays(2)).SelectMany(x => x.Tickets).ToList();
            var threeDaysAgoTickets = schedules.FindAll(x => x.Date == today - TimeSpan.FromDays(3)).SelectMany(x => x.Tickets).ToList();
            decimal yeasterdayRevenue = yeasterdayTickets.Sum(x => GetTicketPrice(x));
            decimal twoDaysAgoRevenue = twoDaysAgoTickets.Sum(x => GetTicketPrice(x));
            decimal threeDaysAgoRevenue = threeDaysAgoTickets.Sum(x => GetTicketPrice(x));

            yesterday_tb.Text = $"Yesterday: {yeasterdayRevenue.ToString("c", new CultureInfo("en-US"))}";
            two_days_ago_tb.Text = $"Two days ago: {twoDaysAgoRevenue.ToString("c", new CultureInfo("en-US"))}";
            three_days_ago_tb.Text = $"Three days ago: {threeDaysAgoRevenue.ToString("c", new CultureInfo("en-US"))}";

            var thisWeekSchedules = schedules.FindAll(x => x.Date > today - TimeSpan.FromDays(7));
            var lastWeekSchedules = schedules.FindAll(x => x.Date > today - TimeSpan.FromDays(14) && x.Date < today - TimeSpan.FromDays(7));
            var twoWeekAgoSchedules = schedules.FindAll(x => x.Date > today - TimeSpan.FromDays(21) && x.Date < today - TimeSpan.FromDays(14));
            double thisWeekEmpty = (double)(thisWeekSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) - thisWeekSchedules.SelectMany(x => x.Tickets).Count()) / thisWeekSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) * 100.0;
            double lastWeekEmpty = (double)(lastWeekSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) - lastWeekSchedules.SelectMany(x => x.Tickets).Count()) / lastWeekSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) * 100.0;
            double twoWeekAgoEmpty = (double)(twoWeekAgoSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) - twoWeekAgoSchedules.SelectMany(x => x.Tickets).Count()) / twoWeekAgoSchedules.Select(x => x.Aircrafts).Sum(x => x.TotalSeats) * 100.0;

            this_week_tb.Text = $"This week: {(int)thisWeekEmpty} %";
            last_week_tb.Text = $"Last week: {(int)lastWeekEmpty} %";
            two_weeks_ago_tb.Text = $"Two weeks ago: {(int)twoWeekAgoEmpty} %";
        }

        private int GetTicketPrice(Tickets ticket)
        {
            switch(ticket.CabinTypeID)
            {
                case 2:
                    return (int)Math.Floor(ticket.Schedules.EconomyPrice * 1.35m);
                case 3:
                    return (int)Math.Floor(ticket.Schedules.EconomyPrice * 1.35m * 1.3m);
                default:
                    return (int)ticket.Schedules.EconomyPrice;
            }
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime startTime = DateTime.Now;

            GenerateSummary();

            DateTime endTime = DateTime.Now;
            DateTime spentTime = new DateTime(2000, 1, 1) + (endTime - startTime);
            generation_time_tb.Text = $"Report generated in {spentTime.ToString("s.fff")} seconds";
        }
    }
}
