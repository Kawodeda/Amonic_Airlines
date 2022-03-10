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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AMONIC_Session5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const bool restrictShownSchedules = false;

        private Schedules selectedSchedule;
        private string bookingReference;
        private int itemsCount = 0;
        private decimal taxes = 0m;
        private decimal total = 0m;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ok_btn_Click(object sender, RoutedEventArgs e)
        {
            bookingReference = booking_reference_tb.Text;
            var tickets = DBContextProvider.Context.Tickets.ToList().FindAll(x => x.BookingReference == bookingReference);

            if(tickets.Count != 0)
            {
                var schedules = tickets.Select(x => x.Schedules).Distinct().ToList().FindAll(x => restrictShownSchedules ? (x.Date + x.Time - DateTime.Now > TimeSpan.FromDays(1)) : true);
                flights_cb.ItemsSource = schedules;
                flights_cb.SelectedIndex = 0;

                if(schedules.Count == 0)
                {
                    MessageBox.Show("Вы больше не можете приобрести удобства для рейсов по этому бронированию", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Бронирования с данным номером не найдено", "", MessageBoxButton.OK, MessageBoxImage.Information);
                booking_reference_tb.Clear();
            }
        }

        private void show_amenities_btn_Click(object sender, RoutedEventArgs e)
        {
            string priceFree = "Free";

            if(flights_cb.SelectedItem is Schedules schedule)
            {
                selectedSchedule = schedule;
                var ticket = DBContextProvider.Context.Tickets.ToList().FindAll(x => x.BookingReference == bookingReference && x.Schedules == selectedSchedule).FirstOrDefault();

                if(ticket != null)
                {
                    itemsCount = 0;
                    ticket_info_1_tb.Text = $"Full name: {ticket.Lastname} {ticket.Firstname}   Passport number: {ticket.PassportNumber}";
                    ticket_info_2_tb.Text = $"Your cabin class is: {ticket.CabinTypes.Name}";

                    var amenities = ticket.CabinTypes.Amenities.ToList();
                    var boughtAmenities = ticket.AmenitiesTickets.ToList();

                    amenities_stack_panel.Children.Clear();
                    foreach(var amenity in amenities)
                    {
                        AmenityTag tag = new AmenityTag(amenity);
                        CheckBox checkBox = new CheckBox();
                        checkBox.Content = $"{amenity.Service} ( {(amenity.Price == 0m ? priceFree : amenity.Price.ToString("c", new CultureInfo("en-US")))} )";
                        checkBox.Margin = new Thickness(0, 5, 0, 0);
                        if (boughtAmenities.Select(x => x.Amenities).Contains(amenity))
                        {
                            checkBox.IsChecked = true;
                            itemsCount++;
                            tag.Payed = true;
                        }
                        checkBox.Checked += CheckBox_Checked;
                        checkBox.Unchecked += CheckBox_Checked;
                        checkBox.Tag = tag;
                        if (amenity.Price == 0m)
                        {
                            checkBox.IsChecked = true;
                            checkBox.IsEnabled = false;
                        }

                        amenities_stack_panel.Children.Add(checkBox);
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox checkBox)
            {
                if(checkBox.Tag is AmenityTag amenity)
                {
                    if(checkBox.IsChecked == true)
                    {
                        itemsCount++;
                        
                    }
                    else
                    {
                        itemsCount--;

                    }
                    taxes = CalculateTaxes();
                    total = CalculateAmount();
                }
            }

            items_tb.Text = $"Items selected: {itemsCount}";
            duties_tb.Text = $"Duties and taxes: {taxes.ToString("c", new CultureInfo("en-US"))}";
            total_tb.Text = $"Total payable: {(total >= 0m ? total.ToString("c", new CultureInfo("en-US")) : $"refund {total.ToString("c", new CultureInfo("en-US"))}")}";
        }

        private decimal CalculateTaxes()
        {
            decimal tax = 0m;

            foreach(var child in amenities_stack_panel.Children)
            {
                if(child is CheckBox checkBox)
                {
                    if(checkBox.IsChecked == true && checkBox.Tag is AmenityTag amenity)
                    {
                        if(!amenity.Payed)
                        tax += amenity.Amenity.Price * 0.05m;
                    }
                }
            }

            return tax;
        }

        private decimal CalculateAmount()
        {
            decimal amount = 0m;

            foreach (var child in amenities_stack_panel.Children)
            {
                if (child is CheckBox checkBox)
                {
                    if (checkBox.Tag is AmenityTag amenity)
                    {
                        if(checkBox.IsChecked == true)
                        {
                            if (!amenity.Payed)
                                amount += amenity.Amenity.Price + amenity.Amenity.Price * 0.05m;
                        }
                        else
                        {
                            if (amenity.Payed)
                                amount -= amenity.Amenity.Price + amenity.Amenity.Price * 0.05m;
                        }
                    }
                }
            }

            return amount;
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ticket = DBContextProvider.Context.Tickets.ToList().FindAll(x => x.BookingReference == bookingReference && x.Schedules == selectedSchedule).FirstOrDefault();

                if (ticket != null)
                {
                    foreach (var child in amenities_stack_panel.Children)
                    {
                        if (child is CheckBox checkBox)
                        {
                            if (checkBox.Tag is AmenityTag amenity)
                            {
                                if (checkBox.IsChecked == true)
                                {
                                    if (!amenity.Payed)
                                    {
                                        AmenitiesTickets amenitiesTickets = new AmenitiesTickets();
                                        amenitiesTickets.Amenities = amenity.Amenity;
                                        amenitiesTickets.Tickets = ticket;
                                        amenitiesTickets.Price = amenity.Amenity.Price;

                                        DBContextProvider.Context.AmenitiesTickets.Add(amenitiesTickets);
                                    }
                                }
                                else
                                {
                                    if (amenity.Payed)
                                    {
                                        var deleteAmenity = DBContextProvider.Context.AmenitiesTickets.ToList().Find(x => x.Tickets == ticket && x.Amenities == amenity.Amenity);
                                        DBContextProvider.Context.AmenitiesTickets.Remove(deleteAmenity);
                                    }
                                }
                            }
                        }
                    }

                    DBContextProvider.Context.SaveChanges();
                    MessageBox.Show("Данные добавлены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось добавить данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void report_btn_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow().ShowDialog();
        }

        private void sumary_btn_Click(object sender, RoutedEventArgs e)
        {
            new ShortSummaryWindow().ShowDialog();
        }
    }
}
