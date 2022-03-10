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
    /// Логика взаимодействия для SchedulesWindow.xaml
    /// </summary>
    public partial class SchedulesWindow : Window
    {
        private List<Schedules> schedules;
        private List<Airports> airports;
        public SchedulesWindow()
        {
            InitializeComponent();

            airports = DbContextProvider.Context.Airports.ToList();
            airports = airports.Prepend(Airports.AllAirports).ToList();
            airport_from_cb.ItemsSource = airports;
            airport_from_cb.SelectedIndex = 0;

            sort_cb.ItemsSource = SchedlueSortType.SortTypes;
            sort_cb.SelectedItem = SchedlueSortType.ByDateTime;

            SetDefaultFilter();
            ApplyFilter();
        }

        private void UpdateSchedulesDataGrid()
        {
            schedules_data_grid.ItemsSource = schedules;
        }

        private void SetDefaultFilter()
        {
            airport_from_cb.SelectedItem = Airports.AllAirports;
            airport_to_cb.SelectedItem = Airports.AllAirports;
            sort_cb.SelectedItem = SchedlueSortType.ByDateTime;
            outbound_tb.Text = "";
            flight_number_tb.Text = "";
        }

        private void ApplyFilter()
        {
            schedules = DbContextProvider.Context.Schedules.ToList();

            if (airport_from_cb.SelectedItem is Airports airportFrom)
            {
                schedules = schedules.FindAll(x => airportFrom == Airports.AllAirports ? true : x.Routes.Airports == airportFrom);
            }

            if (airport_to_cb.SelectedItem is Airports airportTo)
            {
                schedules = schedules.FindAll(x => airportTo == Airports.AllAirports ? true : x.Routes.Airports1 == airportTo);
            }

            if(outbound_tb.Text != "")
            {
                DateTime outboundDate;

                if(DateTime.TryParse(outbound_tb.Text, out outboundDate))
                {
                    schedules = schedules.FindAll(x => x.Date + x.Time <= outboundDate);
                }
                else
                {
                    MessageBox.Show("Введен неверный формат даты", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            if(flight_number_tb.Text != "")
            {
                schedules = schedules.FindAll(x => x.FlightNumber == flight_number_tb.Text);
            }

            if (sort_cb.SelectedItem is SchedlueSortType sortType)
            {
                schedules.Sort(sortType.Comparison);
            }

            UpdateSchedulesDataGrid();
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            if(schedules_data_grid.SelectedItem is Schedules schedule)
            {
                schedule.Confirmed = !schedule.Confirmed;
                DbContextProvider.Context.SaveChanges();
                ApplyFilter();
            }
            else
            {
                MessageBox.Show("Вы должны выбрать объект перед этим дейстивием", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (schedules_data_grid.SelectedItem is Schedules schedule)
            {
                switch (new ScheduleEditWindow(schedule).ShowDialog())
                {
                    case true:
                        MessageBox.Show("Данные успешно обновлены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        ApplyFilter();
                        break;
                    case null:
                        MessageBox.Show("Не удалось обновить данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Вы должны выбрать объект перед этим дейстивием", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void import_btn_Click(object sender, RoutedEventArgs e)
        {
            new ImportWindow().ShowDialog();
            ApplyFilter();
        }

        private void airport_from_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (airport_from_cb.SelectedItem is Airports airport)
            {
                airport_to_cb.ItemsSource = airports.FindAll(x => airport == Airports.AllAirports ? true : x != airport);
                airport_to_cb.SelectedIndex = airport == Airports.AllAirports ? 0 : 1;
            }
        }

        private void default_filter_btn_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultFilter();
        }

        private void apply_filter_btn_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }
    }
}
