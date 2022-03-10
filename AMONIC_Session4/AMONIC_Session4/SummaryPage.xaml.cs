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

namespace AMONIC_Session4
{
    /// <summary>
    /// Логика взаимодействия для SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {
        public SummaryPage()
        {
            InitializeComponent();

            var surveys = DbContextProvider.Context.Survey.ToList();

            sample_size_tb.Text = $"Sample Size: {surveys.Count} Adults";

            DateTime lowerDate = (DateTime)surveys.First().Date;
            DateTime upperDate = (DateTime)surveys.First().Date;

            foreach(var survey in surveys)
            {
                if(survey.Date != null ? survey.Date > upperDate : false)
                {
                    upperDate = (DateTime)survey.Date;
                }
                if (survey.Date != null ? survey.Date < lowerDate : false)
                {
                    lowerDate = (DateTime)survey.Date;
                }
            }

            field_work_tb.Text = $"Field work: {lowerDate.ToString("Y", new CultureInfo("en-US"))} - {upperDate.ToString("Y", new CultureInfo("en-US"))}";

            List<List<int>> gender = new List<List<int>>();
            gender.Add(new int[] { surveys.FindAll(x => x.Gender == "M").Count, surveys.FindAll(x => x.Gender == "F").Count }.ToList());
            gender_dg.ItemsSource = gender;

            List<List<int>> age = new List<List<int>>();
            List<int> ageRow = new List<int>();
            ageRow.Add(surveys.FindAll(x => x.Age != "" ? Convert.ToInt32(x.Age) >= 18 && Convert.ToInt32(x.Age) <= 24 : false).Count);
            ageRow.Add(surveys.FindAll(x => x.Age != "" ? Convert.ToInt32(x.Age) >= 25 && Convert.ToInt32(x.Age) <= 39 : false).Count);
            ageRow.Add(surveys.FindAll(x => x.Age != "" ? Convert.ToInt32(x.Age) >= 40 && Convert.ToInt32(x.Age) <= 59 : false).Count);
            ageRow.Add(surveys.FindAll(x => x.Age != "" ? Convert.ToInt32(x.Age) >= 60 : false).Count);
            age.Add(ageRow);
            age_dg.ItemsSource = age;

            List<List<int>> cabinType = new List<List<int>>();
            List<int> cabinTypeRow = new List<int>();
            cabinTypeRow.Add(surveys.FindAll(x => x.CabinTypeID == 1).Count);
            cabinTypeRow.Add(surveys.FindAll(x => x.CabinTypeID == 2).Count);
            cabinTypeRow.Add(surveys.FindAll(x => x.CabinTypeID == 3).Count);
            cabinType.Add(cabinTypeRow);
            cabin_type_dg.ItemsSource = cabinType;

            List<List<int>> destination = new List<List<int>>();
            List<int> destinationRow = new List<int>();
            destinationRow.Add(surveys.FindAll(x => x.ArrivalId == 2).Count);
            destinationRow.Add(surveys.FindAll(x => x.ArrivalId == 4).Count);
            destinationRow.Add(surveys.FindAll(x => x.ArrivalId == 6).Count);
            destinationRow.Add(surveys.FindAll(x => x.ArrivalId == 7).Count);
            destinationRow.Add(surveys.FindAll(x => x.ArrivalId == 3).Count);
            destination.Add(destinationRow);
            destination_dg.ItemsSource = destination;
        }
    }
}
