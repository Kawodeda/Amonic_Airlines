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

namespace AMONIC_Session5
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void make_report_btn_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> stats = new List<List<string>>();

            if (mode_1_rbtn.IsChecked == true)
            {
                DateTime date;
                if(DateTime.TryParse(date_tb.Text, out date))
                {
                    Schedules schedule = DBContextProvider.Context.Schedules.ToList().Find(x => x.Date == date && x.FlightNumber == flight_tb.Text);

                    if(schedule != null)
                    {
                        var cabinTypes = DBContextProvider.Context.CabinTypes.ToList();
                        var amenities = DBContextProvider.Context.Amenities.ToList();

                        foreach(var cabinType in cabinTypes)
                        {
                            List<string> statsRow = new List<string>();
                            statsRow.Add(cabinType.Name);

                            foreach(var amenity in amenities)
                            {
                                statsRow.Add(cabinType.Amenities.Contains(amenity) ? schedule.Tickets.ToList().FindAll(z => z.CabinTypes == cabinType).Sum(x => x.AmenitiesTickets.ToList().FindAll(y => y.Amenities == amenity).Count).ToString() : "-");
                            }

                            stats.Add(statsRow);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Рейс не найден", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Введен неверный формат даты", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else if (mode_2_rbtn.IsChecked == true)
            {
                DateTime lowerDate;
                DateTime upperDate;

                if(DateTime.TryParse(from_tb.Text, out lowerDate))
                {
                    if (DateTime.TryParse(to_tb.Text, out upperDate))
                    {
                        var amenitiesTickets = DBContextProvider.Context.Schedules.ToList().FindAll(x => x.Date >= lowerDate && x.Date <= upperDate).SelectMany(x => x.Tickets).SelectMany(x => x.AmenitiesTickets).ToList();
                        var amenities = DBContextProvider.Context.Amenities.ToList();
                        var cabinTypes = DBContextProvider.Context.CabinTypes.ToList();

                        foreach(var cabinType in cabinTypes)
                        {
                            List<string> statsRow = new List<string>();
                            statsRow.Add(cabinType.Name);

                            foreach(var amenity in amenities)
                            {
                                statsRow.Add(amenitiesTickets.FindAll(x => x.Tickets.CabinTypes == cabinType && x.Amenities == amenity).Count.ToString());
                            }

                            stats.Add(statsRow);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введен неверный формат даты", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Введен неверный формат даты", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            report_dg.ItemsSource = stats;
        }

        private void mode_2_rbtn_Checked(object sender, RoutedEventArgs e)
        {
            if(from_tb != null)
                from_tb.IsEnabled = true;
            if (to_tb != null)
                to_tb.IsEnabled = true;
            if (flight_tb != null)
                flight_tb.IsEnabled = false;
            if (date_tb != null)
                date_tb.IsEnabled = false;
        }

        private void mode_1_rbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (from_tb != null)
                from_tb.IsEnabled = false;
            if (to_tb != null)
                to_tb.IsEnabled = false;
            if (flight_tb != null)
                flight_tb.IsEnabled = true;
            if (date_tb != null)
                date_tb.IsEnabled = true;
        }
    }
}
