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

namespace AMONIC_Desktop
{
    /// <summary>
    /// Логика взаимодействия для ScheduleEditWindow.xaml
    /// </summary>
    public partial class ScheduleEditWindow : Window
    {
        private Schedules schedule;

        public ScheduleEditWindow(Schedules _schedule)
        {
            InitializeComponent();

            schedule = _schedule;

            from_tb.Text = $"From: {schedule.Routes.Airports.IATACode}";
            to_tb.Text = $"To: {schedule.Routes.Airports1.IATACode}";
            aircraft_tb.Text = $"From: {schedule.Aircrafts.Name}";
            date_tb.Text = schedule.Date.ToString("d");
            time_tb.Text = schedule.Time.ToString();
            price_tb.Text = ((int)schedule.EconomyPrice).ToString();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date;
                if(DateTime.TryParse(date_tb.Text, out date))
                {
                    schedule.Date = date;
                }
                else
                {
                    MessageBox.Show("Введен неверный формат даты", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                TimeSpan time;
                if(TimeSpan.TryParse(time_tb.Text, out time))
                {
                    schedule.Time = time;
                }
                else
                {
                    MessageBox.Show("Введен неверный формат времени", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                decimal price = 0m;
                if(decimal.TryParse(price_tb.Text, out price))
                {
                    schedule.EconomyPrice = price;
                }
                else
                {
                    MessageBox.Show("Введен неверный формат цены", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                DbContextProvider.Context.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch
            {
                DialogResult = null;
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
