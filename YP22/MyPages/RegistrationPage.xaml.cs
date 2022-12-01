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
using YP22.DBConnect;

namespace YP22.MyPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text.Length > 0 && TbPassword.Text.Trim().Length > 0 &&
                TbFirstname.Text.Length > 0 && TbLastname.Text.Length >0 
                && TbName.Text.Length > 0
                )
            {
                User newUser = new User();
                newUser.Firstname = TbFirstname.Text;
                newUser.LastName = TbLastname.Text;
                newUser.Name = TbName.Text;
                newUser.Password = TbPassword.Text.Trim();
                newUser.LogIn = TbLogin.Text;
                newUser.RoleId = 1;

                DBConnect.ConnectClass.db.User.Add(newUser);
                MessageBox.Show("Регистрация выполнена успешно");
                NavigationService.Navigate(new YP22.MyPages.AuthPage());

            }
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new YP22.MyPages.AuthPage());
        }
    }
}
