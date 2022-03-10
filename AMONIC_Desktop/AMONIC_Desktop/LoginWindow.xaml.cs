using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace AMONIC_Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly TimeSpan blockTimeSpan = TimeSpan.FromSeconds(10);

        private int failedCount = 0;
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime blockTime;
        private bool isBlocked = false;

        public MainWindow()
        {
            InitializeComponent();

            FileInfo logoInfo = new FileInfo("./DS2017_TP09_color@2x.png");

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(logoInfo.FullName);
            logo.EndInit();

            logo_image.Source = logo;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.IsEnabled = true;

            new SchedulesWindow().Show();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string username = username_tb.Text;
            string password = password_tb.Text;

            if(failedCount >= 3)
            {
                if(!isBlocked)
                {
                    isBlocked = true;
                    blockTime = DateTime.Now;
                    LockInput();
                    timer.Start();
                    ShowCountdown(blockTime + blockTimeSpan - DateTime.Now);

                    MessageBox.Show("Слишком много неудачных попыток входа - вход временно заблокирован", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                switch (Authorization.LogIn(username, password))
                {
                    case LoginStatus.Success:
                        switch(Authorization.CurrentUser.RoleID)
                        {
                            case 1:
                                new AdminMainMenu().Show();
                                Close();
                                break;
                            case 2:
                                new UserMainMenu().Show();
                                Close();
                                break;
                        }
                        break;
                    case LoginStatus.IncorrectInput:
                        failedCount++;
                        MessageBox.Show("Введены неверные данные", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    case LoginStatus.Blocked:
                        MessageBox.Show("Вход невозможен, так как вы заблокированы", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    case LoginStatus.Error:
                        MessageBox.Show("Произошла неизвестная ошибка", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            } 
        }

        private void LockInput()
        {
            username_tb.IsEnabled = false;
            password_tb.IsEnabled = false;
            login_btn.IsEnabled = false;
        }

        private void UnlockInput()
        {
            username_tb.IsEnabled = true;
            password_tb.IsEnabled = true;
            login_btn.IsEnabled = true;
        }

        private void ShowCountdown(TimeSpan time)
        {
            DateTime dateTime = new DateTime(2022, 01, 01);
            dateTime += time;

            countdown_tb.Text = dateTime.ToString("mm:ss");
        }

        private void ClearCountdown()
        {
            countdown_tb.Text = "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(blockTime + blockTimeSpan >= DateTime.Now)
            {
                ShowCountdown(blockTime + blockTimeSpan - DateTime.Now);
            }
            else
            {
                timer.Stop();
                isBlocked = false;
                failedCount = 0;
                ClearCountdown();
                UnlockInput();
            }
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
