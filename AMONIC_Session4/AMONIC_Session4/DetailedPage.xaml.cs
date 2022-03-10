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
using System.Windows.Forms.DataVisualization.Charting;

namespace AMONIC_Session4
{
    /// <summary>
    /// Логика взаимодействия для DetailedPage.xaml
    /// </summary>
    public partial class DetailedPage : Page
    {
        public DetailedPage()
        {
            InitializeComponent();

            var surveys = DbContextProvider.Context.Survey.ToList();

            List<TimePeriod> dates = new List<TimePeriod>();
            dates.Add(new TimePeriod(surveys.First().Date.Value));

            foreach(var survey in surveys)
            {
                bool match = false;
                foreach(var date in dates)
                {
                    if(date.Date.Month == survey.Date.Value.Month && date.Date.Year == survey.Date.Value.Year)
                    {
                        match = true;
                        break;
                    }
                }

                if(!match)
                {
                    dates.Add(new TimePeriod(survey.Date.Value));
                }
            }

            dates = dates.Prepend(TimePeriod.AllSurveys).ToList();
            date_cb.ItemsSource = dates;
            date_cb.SelectedIndex = 0;

            gender_cb.ItemsSource = new Gender[] { Gender.AllGenders, Gender.Male, Gender.Female};
            gender_cb.SelectedIndex = 0;
            gender_cb.IsEnabled = false;

            age_cb.ItemsSource = new AgeGroup[] { AgeGroup.AllAges, AgeGroup.Age18to24, AgeGroup.Age25to39, AgeGroup.Age40to59, AgeGroup.Age60};
            age_cb.SelectedIndex = 0;
            age_cb.IsEnabled = false;

            UpdateStats();
        }

        private void UpdateStats()
        {
            const string missingData = "-";
            var surveys = DbContextProvider.Context.Survey.ToList();

            if (date_cb.SelectedItem is TimePeriod date)
            {
                surveys = surveys.FindAll(x => date == TimePeriod.AllSurveys ? true : x.Date.Value.Month == date.Date.Month && x.Date.Value.Year == date.Date.Year);
            }

            if (gender_check.IsChecked == true && gender_cb.SelectedItem is Gender gender)
            {
                surveys = surveys.FindAll(x => gender == Gender.AllGenders ? true : Gender.Parse(x.Gender) == gender);
            }

            if (age_check.IsChecked == true && age_cb.SelectedItem is AgeGroup age)
            {
                surveys = surveys.FindAll(x => age == AgeGroup.AllAges ? true : age.CheckAge(x.Age));
            }

            var answers = DbContextProvider.Context.Answer.ToList().FindAll(x => x.Id != 0);
            answers[0].Color = System.Drawing.Color.FromArgb(255, 82, 160, 104);
            answers[1].Color = System.Drawing.Color.FromArgb(255, 166, 222, 169);
            answers[2].Color = System.Drawing.Color.FromArgb(255, 198, 230, 200);
            answers[3].Color = System.Drawing.Color.FromArgb(255, 224, 207, 187);
            answers[4].Color = System.Drawing.Color.FromArgb(255, 226, 193, 193);
            answers[5].Color = System.Drawing.Color.FromArgb(255, 228, 88, 57);
            answers[6].Color = System.Drawing.Color.FromArgb(255, 218, 218, 218);

            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(q1_diagram.Width, q1_diagram.Height);
            System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(q2_diagram.Width, q2_diagram.Height);
            System.Drawing.Bitmap bmp3 = new System.Drawing.Bitmap(q3_diagram.Width, q3_diagram.Height);
            System.Drawing.Bitmap bmp4 = new System.Drawing.Bitmap(q4_diagram.Width, q4_diagram.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp1);
            float nextX1 = 0, nextX2 = 0, nextX3 = 0, nextX4 = 0;
            for (int i = answers.Count - 1; i >= 0; i--)
            {
                System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(answers[i].Color);
                float w1 = (float)surveys.FindAll(s => s.Answer == answers[i]).Count / surveys.Count * bmp1.Width;
                float w2 = (float)surveys.FindAll(s => s.Answer1 == answers[i]).Count / surveys.Count * bmp2.Width;
                float w3 = (float)surveys.FindAll(s => s.Answer2 == answers[i]).Count / surveys.Count * bmp3.Width;
                float w4 = (float)surveys.FindAll(s => s.Answer3 == answers[i]).Count / surveys.Count * bmp4.Width;

                g = System.Drawing.Graphics.FromImage(bmp1);
                g.FillRectangle(brush, nextX1, 0, w1, 10);
                g = System.Drawing.Graphics.FromImage(bmp2);
                g.FillRectangle(brush, nextX2, 0, w2, 10);
                g = System.Drawing.Graphics.FromImage(bmp3);
                g.FillRectangle(brush, nextX3, 0, w3, 10);
                g = System.Drawing.Graphics.FromImage(bmp4);
                g.FillRectangle(brush, nextX4, 0, w4, 10);

                nextX1 += w1;
                nextX2 += w2;
                nextX3 += w3;
                nextX4 += w4;
            }
            q1_diagram.Image = bmp1;
            q2_diagram.Image = bmp2;
            q3_diagram.Image = bmp3;
            q4_diagram.Image = bmp4;

            List<List<string>> q1 = new List<List<string>>(answers.Count);
            List<List<string>> q2 = new List<List<string>>(answers.Count);
            List<List<string>> q3 = new List<List<string>>(answers.Count);
            List<List<string>> q4 = new List<List<string>>(answers.Count);

            foreach (var answer in answers)
            {
                List<string> q1Row = new List<string>();
                List<string> q2Row = new List<string>();
                List<string> q3Row = new List<string>();
                List<string> q4Row = new List<string>();

                q1Row.Add(answer.Name);
                q2Row.Add(answer.Name);
                q3Row.Add(answer.Name);
                q4Row.Add(answer.Name);

                q1Row.Add("");
                q2Row.Add("");
                q3Row.Add("");
                q4Row.Add("");

                if (gender_check.IsChecked == true && gender_cb.SelectedItem is Gender gender1)
                {
                    if (gender1 == Gender.Male)
                    {
                        q1Row.Add(surveys.FindAll(x => x.Answer == answer).Count.ToString());
                        q1Row.Add(missingData);
                        q2Row.Add(surveys.FindAll(x => x.Answer1 == answer).Count.ToString());
                        q2Row.Add(missingData);
                        q3Row.Add(surveys.FindAll(x => x.Answer2 == answer).Count.ToString());
                        q3Row.Add(missingData);
                        q4Row.Add(surveys.FindAll(x => x.Answer3 == answer).Count.ToString());
                        q4Row.Add(missingData);
                    }
                    else if(gender1 == Gender.Female)
                    {
                        q1Row.Add(missingData);
                        q1Row.Add(surveys.FindAll(x => x.Answer == answer).Count.ToString());
                        q2Row.Add(missingData);
                        q2Row.Add(surveys.FindAll(x => x.Answer1 == answer).Count.ToString());
                        q3Row.Add(missingData);
                        q3Row.Add(surveys.FindAll(x => x.Answer2 == answer).Count.ToString());
                        q4Row.Add(missingData);
                        q4Row.Add(surveys.FindAll(x => x.Answer3 == answer).Count.ToString());
                    }
                    else if(gender1 == Gender.AllGenders)
                    {
                        q1Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer == answer).Count.ToString());
                        q1Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer == answer).Count.ToString());
                        q2Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer1 == answer).Count.ToString());
                        q2Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer1 == answer).Count.ToString());
                        q3Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer2 == answer).Count.ToString());
                        q3Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer2 == answer).Count.ToString());
                        q4Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer3 == answer).Count.ToString());
                        q4Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer3 == answer).Count.ToString());
                    }
                }
                else
                {
                    q1Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer == answer).Count.ToString());
                    q1Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer == answer).Count.ToString());
                    q2Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer1 == answer).Count.ToString());
                    q2Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer1 == answer).Count.ToString());
                    q3Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer2 == answer).Count.ToString());
                    q3Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer2 == answer).Count.ToString());
                    q4Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Male && x.Answer3 == answer).Count.ToString());
                    q4Row.Add(surveys.FindAll(x => Gender.Parse(x.Gender) == Gender.Female && x.Answer3 == answer).Count.ToString());
                }

                string age18to24Q1 = surveys.FindAll(x => AgeGroup.Age18to24.CheckAge(x.Age) && x.Answer == answer).Count.ToString();
                string age25to39Q1 = surveys.FindAll(x => AgeGroup.Age25to39.CheckAge(x.Age) && x.Answer == answer).Count.ToString();
                string age40to59Q1 = surveys.FindAll(x => AgeGroup.Age40to59.CheckAge(x.Age) && x.Answer == answer).Count.ToString();
                string age60Q1 = surveys.FindAll(x => AgeGroup.Age60.CheckAge(x.Age) && x.Answer == answer).Count.ToString();

                string age18to24Q2 = surveys.FindAll(x => AgeGroup.Age18to24.CheckAge(x.Age) && x.Answer1 == answer).Count.ToString();
                string age25to39Q2 = surveys.FindAll(x => AgeGroup.Age25to39.CheckAge(x.Age) && x.Answer1 == answer).Count.ToString();
                string age40to59Q2 = surveys.FindAll(x => AgeGroup.Age40to59.CheckAge(x.Age) && x.Answer1 == answer).Count.ToString();
                string age60Q2 = surveys.FindAll(x => AgeGroup.Age60.CheckAge(x.Age) && x.Answer1 == answer).Count.ToString();

                string age18to24Q3 = surveys.FindAll(x => AgeGroup.Age18to24.CheckAge(x.Age) && x.Answer2 == answer).Count.ToString();
                string age25to39Q3 = surveys.FindAll(x => AgeGroup.Age25to39.CheckAge(x.Age) && x.Answer2 == answer).Count.ToString();
                string age40to59Q3 = surveys.FindAll(x => AgeGroup.Age40to59.CheckAge(x.Age) && x.Answer2 == answer).Count.ToString();
                string age60Q3 = surveys.FindAll(x => AgeGroup.Age60.CheckAge(x.Age) && x.Answer2 == answer).Count.ToString();

                string age18to24Q4 = surveys.FindAll(x => AgeGroup.Age18to24.CheckAge(x.Age) && x.Answer3 == answer).Count.ToString();
                string age25to39Q4 = surveys.FindAll(x => AgeGroup.Age25to39.CheckAge(x.Age) && x.Answer3 == answer).Count.ToString();
                string age40to59Q4 = surveys.FindAll(x => AgeGroup.Age40to59.CheckAge(x.Age) && x.Answer3 == answer).Count.ToString();
                string age60Q4 = surveys.FindAll(x => AgeGroup.Age60.CheckAge(x.Age) && x.Answer3 == answer).Count.ToString();

                if (age_check.IsChecked == true && age_cb.SelectedItem is AgeGroup ageGroup)
                {
                    q1Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age18to24 ? age18to24Q1 : missingData);
                    q1Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age25to39 ? age25to39Q1 : missingData);
                    q1Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age40to59 ? age40to59Q1 : missingData);
                    q1Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age60 ? age60Q1 : missingData);

                    q2Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age18to24 ? age18to24Q2 : missingData);
                    q2Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age25to39 ? age25to39Q2 : missingData);
                    q2Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age40to59 ? age40to59Q2 : missingData);
                    q2Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age60 ? age60Q2 : missingData);

                    q3Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age18to24 ? age18to24Q3 : missingData);
                    q3Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age25to39 ? age25to39Q3 : missingData);
                    q3Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age40to59 ? age40to59Q3 : missingData);
                    q3Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age60 ? age60Q3 : missingData);

                    q4Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age18to24 ? age18to24Q4 : missingData);
                    q4Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age25to39 ? age25to39Q4 : missingData);
                    q4Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age40to59 ? age40to59Q4 : missingData);
                    q4Row.Add(ageGroup == AgeGroup.AllAges || ageGroup == AgeGroup.Age60 ? age60Q4 : missingData);
                }
                else
                {
                    q1Row.Add(age18to24Q1);
                    q1Row.Add(age25to39Q1);
                    q1Row.Add(age40to59Q1);
                    q1Row.Add(age60Q1);

                    q2Row.Add(age18to24Q2);
                    q2Row.Add(age25to39Q2);
                    q2Row.Add(age40to59Q2);
                    q2Row.Add(age60Q2);

                    q3Row.Add(age18to24Q3);
                    q3Row.Add(age25to39Q3);
                    q3Row.Add(age40to59Q3);
                    q3Row.Add(age60Q3);

                    q4Row.Add(age18to24Q4);
                    q4Row.Add(age25to39Q4);
                    q4Row.Add(age40to59Q4);
                    q4Row.Add(age60Q4);
                }

                q1Row.Add(surveys.FindAll(x => x.CabinTypeID == 1 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.CabinTypeID == 2 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.CabinTypeID == 3 && x.Answer == answer).Count.ToString());

                q2Row.Add(surveys.FindAll(x => x.CabinTypeID == 1 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.CabinTypeID == 2 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.CabinTypeID == 3 && x.Answer1 == answer).Count.ToString());

                q3Row.Add(surveys.FindAll(x => x.CabinTypeID == 1 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.CabinTypeID == 2 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.CabinTypeID == 3 && x.Answer2 == answer).Count.ToString());

                q4Row.Add(surveys.FindAll(x => x.CabinTypeID == 1 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.CabinTypeID == 2 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.CabinTypeID == 3 && x.Answer3 == answer).Count.ToString());

                q1Row.Add(surveys.FindAll(x => x.ArrivalId == 2 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.ArrivalId == 4 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.ArrivalId == 6 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.ArrivalId == 7 && x.Answer == answer).Count.ToString());
                q1Row.Add(surveys.FindAll(x => x.ArrivalId == 3 && x.Answer == answer).Count.ToString());

                q2Row.Add(surveys.FindAll(x => x.ArrivalId == 2 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.ArrivalId == 4 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.ArrivalId == 6 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.ArrivalId == 7 && x.Answer1 == answer).Count.ToString());
                q2Row.Add(surveys.FindAll(x => x.ArrivalId == 3 && x.Answer1 == answer).Count.ToString());

                q3Row.Add(surveys.FindAll(x => x.ArrivalId == 2 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.ArrivalId == 4 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.ArrivalId == 6 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.ArrivalId == 7 && x.Answer2 == answer).Count.ToString());
                q3Row.Add(surveys.FindAll(x => x.ArrivalId == 3 && x.Answer2 == answer).Count.ToString());

                q4Row.Add(surveys.FindAll(x => x.ArrivalId == 2 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.ArrivalId == 4 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.ArrivalId == 6 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.ArrivalId == 7 && x.Answer3 == answer).Count.ToString());
                q4Row.Add(surveys.FindAll(x => x.ArrivalId == 3 && x.Answer3 == answer).Count.ToString());

                q1Row[1] = surveys.FindAll(x => x.Answer == answer).Count.ToString();
                q2Row[1] = surveys.FindAll(x => x.Answer1 == answer).Count.ToString();
                q3Row[1] = surveys.FindAll(x => x.Answer2 == answer).Count.ToString();
                q4Row[1] = surveys.FindAll(x => x.Answer3 == answer).Count.ToString();

                q1.Add(q1Row);
                q2.Add(q2Row);
                q3.Add(q3Row);
                q4.Add(q4Row);
            }

            q1_dg.ItemsSource = q1;
            q2_dg.ItemsSource = q2;
            q3_dg.ItemsSource = q3;
            q4_dg.ItemsSource = q4;
        }

        private void gender_check_Checked(object sender, RoutedEventArgs e)
        {
            gender_cb.IsEnabled = gender_check.IsChecked == true;
            UpdateStats();
        }

        private void age_check_Checked(object sender, RoutedEventArgs e)
        {
            age_cb.IsEnabled = age_check.IsChecked == true;
            UpdateStats();
        }

        private void gender_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }

        private void age_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }

        private void date_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }
    }
}
