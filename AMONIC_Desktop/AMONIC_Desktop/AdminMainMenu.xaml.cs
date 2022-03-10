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
    /// Логика взаимодействия для AdminMainMenu.xaml
    /// </summary>
    public partial class AdminMainMenu : Window
    {
        private static readonly string allOfficesItem = "All offices";

        public AdminMainMenu()
        {
            InitializeComponent();

            var offices = DbContextProvider.Context.Offices.ToList().Select(x => x.Title);
            offices = offices.Prepend(allOfficesItem);

            office_cb.ItemsSource = offices;
            office_cb.SelectedIndex = 0;

            var users = DbContextProvider.Context.Users.ToList();

            users_grid.ItemsSource = users;
        }

        private void office_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsersDataGrid();
        }

        private void change_role_btn_Click(object sender, RoutedEventArgs e)
        {
            if(users_grid.SelectedItem is Users user)
            {
                new EditRoleWindow(user).ShowDialog();

                UpdateUsersDataGrid();
            }
            else
            {
                MessageBox.Show("Перед этим действием выберите пользователя", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ban_btn_Click(object sender, RoutedEventArgs e)
        {
            if(users_grid.SelectedItem is Users user)
            {
                user.Active = !user.Active;

                DbContextProvider.Context.SaveChanges();
                UpdateUsersDataGrid();
            }
            else
            {
                MessageBox.Show("Перед этим действием выберите пользователя", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void UpdateUsersDataGrid()
        {
            if (office_cb.SelectedItem is string officeTitle)
            {
                var users = DbContextProvider.Context.Users.ToList();

                if (officeTitle != allOfficesItem)
                {
                    users = users.FindAll(x => x.Offices.Title == officeTitle);
                }

                users_grid.ItemsSource = users;
            }
        }

        private void add_user_btn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();

            UpdateUsersDataGrid();
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Authorization.LogOut();
        }
    }
}
