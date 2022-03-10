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
    /// Логика взаимодействия для EditRoleWindow.xaml
    /// </summary>
    public partial class EditRoleWindow : Window
    {
        private Users user;

        public EditRoleWindow(Users _user)
        {
            InitializeComponent();

            user = _user;

            email_tb.Text = user.Email;
            name_tb.Text = user.FirstName;
            last_name_tb.Text = user.LastName;

            var offices = new List<Offices>();
            offices.Add(user.Offices);

            office_cb.ItemsSource = offices;
            office_cb.SelectedIndex = 0;

            switch(user.RoleID)
            {
                case 1:
                    admin_radio_btn.IsChecked = true;
                    break;
                case 2:
                    user_radio_btn.IsChecked = true;
                    break;
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void apply_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(admin_radio_btn.IsChecked == true)
                {
                    user.RoleID = 1;
                }
                else if(user_radio_btn.IsChecked == true)
                {
                    user.RoleID = 2;
                }

                DbContextProvider.Context.SaveChanges();

                MessageBox.Show("Данные обновлены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Произошла неизвестная ошибка, данные не будут обновлены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
