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
    /// Логика взаимодействия для UserMainMenu.xaml
    /// </summary>
    public partial class UserMainMenu : Window
    {
        public UserMainMenu()
        {
            InitializeComponent();

            DateTime spendTime = new DateTime(1, 1, 1);
            var sessions = DbContextProvider.Context.Session.ToList().FindAll(x => x.Users == Authorization.CurrentUser && x.SessionEnd != null && x.SessionEnd.SessionEndStatusId == 1);

            foreach(var session in sessions)
            {
                spendTime += session.SessionEnd.Time - session.StartTime;
            }

            sessions = DbContextProvider.Context.Session.ToList().FindAll(x => x.Users == Authorization.CurrentUser && x != Authorization.CurrentSession && x.SessionEnd == null);

            int crashCount = sessions.Count;

            welcome_tb.Text = $"Hi {Authorization.CurrentUser.FirstName}, Welcome to AMONIC Airlines Automation System.";
            time_tb.Text = $"Time spent on system: {spendTime.ToString("T")}";
            crashes_tb.Text = $"Number of crashes: {crashCount}";

            UpdateSessionGrid();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UpdateSessionGrid()
        {
            var sessions = DbContextProvider.Context.Session.ToList().FindAll(x => x.Users == Authorization.CurrentUser && x != Authorization.CurrentSession);

            foreach(var session in sessions)
            {
                if(session.SessionEnd != null)
                {
                    if(session.SessionEnd.SessionEndStatusId != 1)
                    {
                        sessions.Remove(session);
                    }
                }
            }

            session_grid.ItemsSource = sessions;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Authorization.LogOut();
        }
    }
}
