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

namespace AMONIC_Session3
{
    /// <summary>
    /// Логика взаимодействия для BillingWindow.xaml
    /// </summary>
    public partial class BillingWindow : Window
    {
        private Schedules outboundSchedule;
        private Schedules returnSchedule;
        private List<Passenger> passengers;
        private CabinTypes cabinType;

        public BillingWindow(Schedules _outboundSchedule, Schedules _returnSchedule, CabinTypes _cabinType, List<Passenger> _passengers) : this(_outboundSchedule, _cabinType, _passengers)
        {
            returnSchedule = _returnSchedule;

            decimal amount = outboundSchedule.EconomyPrice * passengers.Count + returnSchedule.EconomyPrice * passengers.Count;
            switch (cabinType.ID)
            {
                case 2:
                    amount = Math.Floor(amount * 1.35m);
                    break;
                case 3:
                    amount = Math.Floor(amount * 1.35m * 1.3m);
                    break;
            }

            amount_tb.Text = $"Total amount:     {amount.ToString("c", new CultureInfo("en-US"))}";
        }

        public BillingWindow(Schedules _outboundSchedule, CabinTypes _cabinType, List<Passenger> _passengers)
        {
            InitializeComponent();

            outboundSchedule = _outboundSchedule;
            cabinType = _cabinType;
            passengers = _passengers;

            decimal amount = outboundSchedule.EconomyPrice * passengers.Count;
            switch(cabinType.ID)
            {
                case 2:
                    amount = Math.Floor(amount * 1.35m);
                    break;
                case 3:
                    amount = Math.Floor(amount * 1.35m * 1.3m);
                    break;
            }

            amount_tb.Text = $"Total amount:     {amount.ToString("c", new CultureInfo("en-US"))}";
        }

        private void issue_btn_Click(object sender, RoutedEventArgs e)
        {
            const int userID = 1;

            try
            {
                List<Schedules> schedules = new List<Schedules>();
                schedules.Add(outboundSchedule);
                if(returnSchedule != null)
                {
                    schedules.Add(returnSchedule);
                }

                string bookingReference = GenerateBookingReferece();

                foreach(var passenger in passengers)
                {
                    foreach(var schedule in schedules)
                    {
                        Tickets ticket = new Tickets();
                        ticket.UserID = userID;
                        ticket.ScheduleID = schedule.ID;
                        ticket.CabinTypeID = cabinType.ID;
                        ticket.Firstname = passenger.FirstName;
                        ticket.Lastname = passenger.LastName;
                        ticket.Phone = passenger.Phone;
                        ticket.PassportNumber = passenger.PassportNumber;
                        ticket.PassportCountryID = passenger.CountryID;
                        ticket.BookingReference = bookingReference;
                        ticket.Confirmed = true;

                        DbContextProvider.Context.Tickets.Add(ticket); 
                    }
                }

                DbContextProvider.Context.SaveChanges();
                MessageBox.Show("Данные успешно добавлены", "Билеты созданы", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Не удалось добавить данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string GenerateBookingReferece()
        {
            const int length = 6;
            var tickets = DbContextProvider.Context.Tickets.ToList();
            var references = tickets.Select(x => x.BookingReference).ToList();

            Random rnd = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string bookingReference = references.First();

            while(references.Contains(bookingReference))
            {
                bookingReference = new string(Enumerable.Repeat(chars, length).Select(x => x[rnd.Next(x.Length)]).ToArray());
            }

            return bookingReference;
        }
    }
}
