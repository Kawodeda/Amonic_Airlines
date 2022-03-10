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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMONIC_Session3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateFromComboBox();
            UpdateToComboBox();
            UpdateCabinTypeComboBox();

            ApplySearchParameters();

            apply_btn.Tag = 1;
        }

        private void UpdateFromComboBox()
        {
            var airports = DbContextProvider.Context.Airports.ToList();
            airports = airports.Prepend(Airports.AllAirports).ToList();

            from_cb.ItemsSource = airports;
            from_cb.SelectedIndex = 0;
        }

        private void UpdateToComboBox()
        {
            if(from_cb.SelectedItem is Airports airport)
            {
                bool isAll = airport == Airports.AllAirports;
                var airports = DbContextProvider.Context.Airports.ToList();
                airports = airports.Prepend(Airports.AllAirports).ToList().FindAll(x => isAll ? true : x != airport); ;

                to_cb.ItemsSource = airports;
                to_cb.SelectedIndex = isAll ? 0 : 1;
            }
        }

        private void UpdateCabinTypeComboBox()
        {
            var cabinTypes = DbContextProvider.Context.CabinTypes.ToList();

            cabin_type_cb.ItemsSource = cabinTypes;
            cabin_type_cb.SelectedIndex = 0;
        }

        private void ApplySearchParameters()
        {
            ApplySearchParameters(outbound_3days_check.IsChecked == true, return_3days_check.IsChecked == true);
        }

        private void ApplySearchParameters(bool showOutbound3days, bool showReturn3days)
        {
            List<Schedules> returnSchedules = new List<Schedules>();
            List<Schedules> outboundSchedules = DbContextProvider.Context.Schedules.ToList();

            DateTime outboundDate = new DateTime();

            if(outbound_tb.Text != "")
            {
                if (DateTime.TryParse(outbound_tb.Text, out outboundDate))
                {
                    outboundSchedules = outboundSchedules.FindAll(x => showOutbound3days ? x.Date >= outboundDate - TimeSpan.FromDays(3) && x.Date <= outboundDate + TimeSpan.FromDays(3) : x.Date == outboundDate);
                }
                else
                {
                    MessageBox.Show("Введен некорректный формат даты", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            if (return_rbtn.IsChecked == true)
            {
                returnSchedules = DbContextProvider.Context.Schedules.ToList(); ;
            }

            DateTime returnDate = new DateTime();

            if (return_tb.Text != "")
            {
                if (DateTime.TryParse(return_tb.Text, out returnDate))
                {
                    returnSchedules = returnSchedules.FindAll(x => showReturn3days ? x.Date >= returnDate - TimeSpan.FromDays(3) && x.Date <= returnDate + TimeSpan.FromDays(3) : x.Date == returnDate);
                }
                else
                {
                    MessageBox.Show("Введен некорректный формат даты", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            if (from_cb.SelectedItem is Airports departure)
            {
                outboundSchedules = outboundSchedules.FindAll(x => departure == Airports.AllAirports ? true : x.Routes.Airports == departure);
                returnSchedules = returnSchedules.FindAll(x => departure == Airports.AllAirports ? true : x.Routes.Airports1 == departure);
            }

            if (to_cb.SelectedItem is Airports destination)
            {
                outboundSchedules = outboundSchedules.FindAll(x => destination == Airports.AllAirports ? true : x.Routes.Airports1 == destination);
                returnSchedules = returnSchedules.FindAll(x => destination == Airports.AllAirports ? true : x.Routes.Airports == destination);
            }

            if (cabin_type_cb.SelectedItem is CabinTypes cabinType)
            {
                apply_btn.Tag = cabinType.ID;
            }

            outbound_dg.ItemsSource = outboundSchedules;
            return_dg.ItemsSource = returnSchedules;
        }

        private void apply_btn_Click(object sender, RoutedEventArgs e)
        {
            ApplySearchParameters();
        }

        private void book_flight_btn_Click(object sender, RoutedEventArgs e)
        {
            int passengersCount = 0;

            if (int.TryParse(passengers_tb.Text, out passengersCount))
            {
                if (outbound_dg.SelectedItem is Schedules outboundSchedule)
                {
                    bool capacityOverflow = false;
                    int outboundCapacity = 0;
                    int returnCapacity = 0;

                    if(cabin_type_cb.SelectedItem is CabinTypes cabinType1)
                    {
                        switch(cabinType1.ID)
                        {
                            case 1:
                                outboundCapacity = outboundSchedule.Aircrafts.EconomySeats;
                                break;
                            case 2:
                                outboundCapacity = outboundSchedule.Aircrafts.BusinessSeats;
                                break;
                            case 3:
                                outboundCapacity = outboundSchedule.Aircrafts.TotalSeats - outboundSchedule.Aircrafts.EconomySeats - outboundSchedule.Aircrafts.BusinessSeats;
                                break;
                        }
                    }

                    if (outboundCapacity < passengersCount)
                    {
                        capacityOverflow = true;
                    }
                    if(return_dg.SelectedItem is Schedules returnSchedule)
                    {
                        if (cabin_type_cb.SelectedItem is CabinTypes cabinType2)
                        {
                            switch (cabinType2.ID)
                            {
                                case 1:
                                    returnCapacity = returnSchedule.Aircrafts.EconomySeats;
                                    break;
                                case 2:
                                    returnCapacity = returnSchedule.Aircrafts.BusinessSeats;
                                    break;
                                case 3:
                                    returnCapacity = returnSchedule.Aircrafts.TotalSeats - outboundSchedule.Aircrafts.EconomySeats - outboundSchedule.Aircrafts.BusinessSeats;
                                    break;
                            }
                        }

                        if (returnCapacity < passengersCount)
                        {
                            capacityOverflow = true;
                        }
                    }

                    if(!capacityOverflow)
                    {
                        if(cabin_type_cb.SelectedItem is CabinTypes cabinTypes)
                        {
                            if (return_dg.SelectedItem is Schedules scheduleReturn)
                            {
                                new ConfirmationWindow(outboundSchedule, scheduleReturn, cabinTypes).ShowDialog();
                            }
                            else
                            {
                                new ConfirmationWindow(outboundSchedule, cabinTypes).ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Число пассажиров превышает число посадочных мест на выбранном рейсе", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Для этого действия выберите рейс/рейсы", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Введите число пассажиров", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void return_3days_check_Checked(object sender, RoutedEventArgs e)
        {
            ApplySearchParameters();
        }

        private void outbound_3days_check_Checked(object sender, RoutedEventArgs e)
        {
            ApplySearchParameters();
        }

        private void from_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateToComboBox();
        }

        private void return_rbtn_Checked(object sender, RoutedEventArgs e)
        {
            if(return_tb != null)
            return_tb.IsEnabled = return_rbtn.IsChecked == true;
        }
    }
}
