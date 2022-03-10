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
    /// Логика взаимодействия для CrashReportWindow.xaml
    /// </summary>
    public partial class CrashReportWindow : Window
    {
        private Session session;

        public CrashReportWindow(Session _session)
        {
            InitializeComponent();

            session = _session;
            info_tb.Text = $"No logout detected for your last login on {session.StartTime.ToString("d")} at {session.StartTime.ToString("t")}";
        }

        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Error error = new Error();
                error.Description = description_tb.Text;

                if (software_rbtn.IsChecked == true)
                {
                    error.Name = "Software Crash";
                }
                else if (system_rbtn.IsChecked == true)
                {
                    error.Name = "System Crash";
                }

                error.Session.Add(session);

                DbContextProvider.Context.Error.Add(error);
                DbContextProvider.Context.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch
            {
                DialogResult = null;
                Close();
            }
        }
    }
}
