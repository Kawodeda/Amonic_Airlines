using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        private Schedules outboundSchedule;
        private Schedules returnSchedule;
        private CabinTypes cabinType;
        private List<Passenger> passengers = new List<Passenger>();

        public ConfirmationWindow(Schedules _outboundSchedule, CabinTypes _cabinType)
        {
            InitializeComponent();

            outboundSchedule = _outboundSchedule;
            cabinType = _cabinType;

            ShowOutboundSchedule();

            var counties = DbContextProvider.Context.Countries.ToList();
            country_cb.ItemsSource = DbContextProvider.Context.Countries.ToList();
            country_cb.SelectedIndex = 0;

            passengers_dg.ItemsSource = passengers;
        }

        public ConfirmationWindow(Schedules _outboundSchedule, Schedules _returnSchedule, CabinTypes _cabinType) : this(_outboundSchedule, _cabinType)
        {
            returnSchedule = _returnSchedule;

            ShowReturnSchedule();
        }

        private void ShowOutboundSchedule()
        {
            outbound_from_tb.Text = $"From: {outboundSchedule.Routes.Airports.IATACode}";
            outbound_to_tb.Text = $"To: {outboundSchedule.Routes.Airports1.IATACode}";
            outbound_cabin_tb.Text = $"Cabin Type: {cabinType.Name}";
            outbound_date_tb.Text = $"Date: {outboundSchedule.Date.ToString("d")}";
            outbound_flight_num_tb.Text = $"Flight number: {outboundSchedule.FlightNumber}";
        }

        private void ShowReturnSchedule()
        {
            return_from_tb.Text = $"From: {returnSchedule.Routes.Airports.IATACode}";
            return_to_tb.Text = $"To: {returnSchedule.Routes.Airports1.IATACode}";
            return_cabin_tb.Text = $"Cabin Type: {cabinType.Name}";
            return_date_tb.Text = $"Date: {returnSchedule.Date.ToString("d")}";
            return_flight_num_tb.Text = $"Flight number: {returnSchedule.FlightNumber}";
        }

        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            if(passengers_dg.SelectedItem is Passenger passenger)
            {
                passengers = passengers.FindAll(x => x != passenger);
                passengers_dg.ItemsSource = passengers;
            }
            else
            {
                MessageBox.Show("Выберите пассажира", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if(name_tb.Text != "" && lastname_tb.Text != "" && birthdate_tb.Text != "" && passport_tb.Text != "" && phone_tb.Text != "" && country_cb.SelectedItem is Countries country)
            {
                Passenger passenger = new Passenger();
                passenger.FirstName = name_tb.Text;
                passenger.LastName = lastname_tb.Text;

                DateTime date;
                if(DateTime.TryParse(birthdate_tb.Text, out date))
                {
                    passenger.Birtdate = date;
                }
                else
                {
                    MessageBox.Show("Введен некорректный формат даты рождения", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                passenger.PassportNumber = passport_tb.Text;
                passenger.CountryID = country.ID;
                passenger.Country = country;
                passenger.Phone = phone_tb.Text;

                passengers = passengers.Append(passenger).ToList();
                passengers_dg.ItemsSource = passengers;
            }
            else
            {
                MessageBox.Show("Не все обязательные поля заполнены", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            if(returnSchedule != null)
            {
                new BillingWindow(outboundSchedule, returnSchedule, cabinType, passengers).ShowDialog();
            }
            else
            {
                new BillingWindow(outboundSchedule, cabinType, passengers).ShowDialog();
            }
        }
    }
}
