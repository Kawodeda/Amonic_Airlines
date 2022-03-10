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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();

            var offices = DbContextProvider.Context.Offices.ToList();

            office_cb.ItemsSource = offices;
            office_cb.SelectedIndex = 0;
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (email_tb.Text != "" &&
                name_tb.Text != "" &&
                last_name_tb.Text != "" &&
                birthdate_tb.Text != "" &&
                password_tb.Text != "")
            {
                if(office_cb.SelectedItem is Offices office)
                {
                    try
                    {
                        Users user = new Users();
                        int latestId = DbContextProvider.Context.Users.ToList().Last().ID;
                        user.ID = latestId + 1;
                        user.Email = email_tb.Text;
                        user.FirstName = name_tb.Text;
                        user.LastName = last_name_tb.Text;
                        user.Offices = office;
                        user.Password = PasswordConverter.Encrypt(password_tb.Text);
                        user.RoleID = 2;

                        DateTime birthdate;

                        if(DateTime.TryParse(birthdate_tb.Text, out birthdate))
                        {
                            user.Birthdate = birthdate;
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод даты рождения", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }

                        user.Active = true;

                        DbContextProvider.Context.Users.Add(user);
                        DbContextProvider.Context.SaveChanges();

                        email_tb.Clear();
                        name_tb.Clear();
                        last_name_tb.Clear();
                        birthdate_tb.Clear();
                        password_tb.Clear();
                        office_cb.SelectedIndex = 0;

                        MessageBox.Show("Данные успешно добавлены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла неизвестная ошибка, данные не будут отправлены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все необходимые поля заполнены", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void office_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
