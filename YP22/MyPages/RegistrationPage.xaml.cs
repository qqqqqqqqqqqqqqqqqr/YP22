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
            BtnReg.IsEnabled = false;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text.Length > 0 && TbPassword.Text.Trim().Length > 0 &&
                TbFirstname.Text.Length > 0  
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
                DBConnect.ConnectClass.db.SaveChanges();
                MessageBox.Show("Регистрация выполнена успешно");
                NavigationService.Navigate(new YP22.MyPages.AuthPage());

            }
          
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new YP22.MyPages.AuthPage());
        }
        bool save1 = false; /*отвечает за условия пароля*/
        bool save2 = false; /*отвечает за условия логина*/

        private void TbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            save1 = false;
            String password = TbPassword.Text;
            bool b1 = false;
            bool b2 = false;
            bool b3 = false;

            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            //toolTipPanel.Children.Add(new TextBlock { Text = "Уведомление", FontSize = 16 });
            toolTipPanel.Children.Add(new TextBlock { Text = "Ваш пароль должен соответсвовать всем требованиям:" });
            toolTipPanel.Children.Add(new TextBlock { Text = "1. Минимум 6 символов." });
            toolTipPanel.Children.Add(new TextBlock { Text = "2. Минимум 1 прописная буква." });
            toolTipPanel.Children.Add(new TextBlock { Text = "3. Минимум 1 цифра." });
            toolTipPanel.Children.Add(new TextBlock { Text = "4. Минимум один символ из набора: ! @ # $ % ^. ." });
            toolTip.Content = toolTipPanel;

            if (password.Length > 6)
            {
                 b1 = false;
                 b2 = false;
                 b3 = false;
                foreach (char a in password)
                {

                    if(a.ToString() == a.ToString().ToUpper())
                    {
                        b1 = true;
                    }

                   if (a.ToString() == "0" || a.ToString() == "1" || a.ToString() == "2" || a.ToString() == "3" || a.ToString() == "4" || a.ToString() == "5" ||
                        a.ToString() == "6" || a.ToString() == "7" || a.ToString() == "8" || a.ToString() == "9")
                    {
                        b2 = true;
                    }
                   if (a.ToString() == "!" || a.ToString() == "@" || a.ToString() == "#" || a.ToString() == "$" || a.ToString() == "%" || a.ToString() == "^")
                    {
                        b3 = true;
                    }
                }
                if(b1 == true && b2==true && b3 == true)
                {
                    save1 = true;
                    Im2.Visibility = Visibility.Visible;
                }
                else
                {
                    TbPassword.ToolTip = toolTip;
                    Im2.Visibility = Visibility.Hidden;
                    save1 = false;

                    BtnReg.IsEnabled = false;
                }
            }
            else
            {
                Im2.Visibility = Visibility.Hidden;

                TbPassword.ToolTip = toolTip;
                save1 = false;
                BtnReg.IsEnabled = false;
            }

            if (TbPassword.Text.Length < 1)
            {
                TbPassword.ToolTip = toolTip;
                Im2.Visibility = Visibility.Hidden;
                save1 = false;

                BtnReg.IsEnabled = false;
            }
            Up();

        }

        private void TbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            save2 = false;
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipPanel = new StackPanel();
            //toolTipPanel.Children.Add(new TextBlock { Text = "Уведомление", FontSize = 16 });
            toolTipPanel.Children.Add(new TextBlock { Text = "Ваш логин должен быть уникальным" });
         
            toolTip.Content = toolTipPanel;
           User UserInDB = ConnectClass.db.User.Where(x => x.LogIn.ToString() == TbLogin.Text.ToString()).FirstOrDefault();
            
            if (UserInDB == null)
            {
                save2 = true;
                Im1.Visibility = Visibility.Visible;
            }
            else
               
            {
                TbLogin.ToolTip = toolTip;
                Im1.Visibility = Visibility.Hidden;
                save2 = false;


            }

            if (TbLogin.Text.Length < 1)
            {
                TbLogin.ToolTip = toolTip;
                Im1.Visibility = Visibility.Hidden;
                save2 = false;

            }

            Up();

        }

        private void Up()
        {
            if (save1 == true && save2 == true && TbName.Text.Length>0 && TbFirstname.Text.Length > 0)
            {
                BtnReg.IsEnabled = true;
            }
            else
            {
                BtnReg.IsEnabled = false;
            }
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {

            Up();
        }

        private void TbFirstname_TextChanged(object sender, TextChangedEventArgs e)
        {

            Up();
        }

        private void TbLastname_TextChanged(object sender, TextChangedEventArgs e)
        {

            Up();
        }
    }
}
