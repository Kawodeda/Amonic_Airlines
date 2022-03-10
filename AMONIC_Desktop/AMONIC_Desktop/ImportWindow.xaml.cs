using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace AMONIC_Desktop
{
    /// <summary>
    /// Логика взаимодействия для ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        private FileInfo currentFile;
        private int duplicateCount = 0, discardedCount = 0, successfulCount = 0;

        public ImportWindow()
        {
            InitializeComponent();
        }

        private void import_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                duplicateCount = 0; 
                discardedCount = 0; 
                successfulCount = 0;

                List<string> lines = new List<string>();

                using (StreamReader reader = new StreamReader(new FileInfo(path_tb.Text).FullName))
                {
                    while(!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }

                foreach(string line in lines)
                {
                    string[] splitLine = line.Split(',');
                    var routes = DbContextProvider.Context.Routes.ToList();

                    try
                    {
                        var schedules = DbContextProvider.Context.Schedules.ToList();

                        switch (splitLine[0])
                        {
                            case "ADD":
                                Schedules schedule = new Schedules();
                                schedule.Date = DateTime.Parse(splitLine[1]);
                                schedule.Time = TimeSpan.Parse(splitLine[2]);
                                schedule.FlightNumber = splitLine[3];
                                schedule.Routes = routes.Find(x => x.Airports.IATACode == splitLine[4] && x.Airports1.IATACode == splitLine[5]);
                                schedule.AircraftID = Convert.ToInt32(splitLine[6]);
                                schedule.EconomyPrice = Convert.ToDecimal(splitLine[7].Split('.')[0] + "," + splitLine[7].Split('.')[1]);
                                switch (splitLine[8])
                                {
                                    case "OK":
                                        schedule.Confirmed = true;
                                        break;
                                    case "CANCELLED":
                                        schedule.Confirmed = false;
                                        break;
                                }

                                foreach (var flight in schedules)
                                {
                                    if (flight.FlightNumber == schedule.FlightNumber && flight.Date == schedule.Date)
                                    {
                                        duplicateCount++;
                                        break;
                                    }
                                }

                                DbContextProvider.Context.Schedules.Add(schedule);
                                DbContextProvider.Context.SaveChanges();
                                successfulCount++;
                                break;
                            case "EDIT":
                                DateTime date = DateTime.Parse(splitLine[1]);
                                string flightNumber = splitLine[3];

                                var editSchedule = schedules.Find(x => x.Date == date && x.FlightNumber == flightNumber);

                                if(editSchedule != null)
                                {
                                    editSchedule.Date = DateTime.Parse(splitLine[1]);
                                    editSchedule.Time = TimeSpan.Parse(splitLine[2]);
                                    editSchedule.FlightNumber = splitLine[3];
                                    editSchedule.Routes = routes.Find(x => x.Airports.IATACode == splitLine[4] && x.Airports1.IATACode == splitLine[5]);
                                    editSchedule.AircraftID = Convert.ToInt32(splitLine[6]);
                                    editSchedule.EconomyPrice = Convert.ToDecimal(splitLine[7].Split('.')[0] + "," + splitLine[7].Split('.')[1]);
                                    switch (splitLine[8])
                                    {
                                        case "OK":
                                            editSchedule.Confirmed = true;
                                            break;
                                        case "CANCELLED":
                                            editSchedule.Confirmed = false;
                                            break;
                                    }

                                    DbContextProvider.Context.SaveChanges();
                                    successfulCount++;
                                }
                                break;
                        }
                    }
                    catch
                    {
                        discardedCount++;
                        continue;
                    }
                }

                changes_tb.Text = $"Successful Changes Applied:   {successfulCount}";
                duplicate_tb.Text = $"Duplicate Records Discarded:   {duplicateCount}";
                missing_field_tb.Text = $"Record with missing fields discarded:   {discardedCount}";

                MessageBox.Show("Данные импортированы", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
            catch
            {
                MessageBox.Show("Не удалось импортировать данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}

        private void open_file_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text files (*.txt;*.csv)|*.txt;*.csv";

                if (dialog.ShowDialog() == true)
                {
                    currentFile = new FileInfo(dialog.FileName);

                    path_tb.Text = currentFile.FullName;
                    path_tb.Focus();
                    path_tb.CaretIndex = path_tb.Text.Length;
                }
            }
            catch
            {
                MessageBox.Show("Не удалось открыть файл", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
