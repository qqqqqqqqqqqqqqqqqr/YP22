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
using YP22.Classes;
using YP22.MyWindow;
using System.Timers;
using System.Windows.Threading;

namespace YP22.MyPages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        int count = 0;
        public AuthPage()
        {
            InitializeComponent();

        }

        DispatcherTimer timer = new DispatcherTimer();

     
        public void isVisibleBtn(object sender, EventArgs e)
        {
            RegPage.IsEnabled = true;
            Auth.IsEnabled = true;
            count = 0;
            timer.Stop();
       
        }
        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            if(TbLogin.Text.Length > 0 && TbPassword.Text.Trim().Length > 0)
            {
                User user = new User();
                user = DBConnect.ConnectClass.db.User.Where(x => TbLogin.Text.Length > 0 && TbPassword.Text.Length > 0 && (x.LogIn == TbLogin.Text && x.Password.Trim() == TbPassword.Text.Trim())).FirstOrDefault();
                if (user == null)
                {
                    if (count < 2)
                    {
                        count++;
                        MessageBox.Show("Данные введены неверно. Попыток до блокировки: " + (3 - count) + ".");
                        TbLogin.Text = null;
                        TbPassword.Text = null;
                    }
                    else if (count == 2)
                    {
                        MessageBox.Show("Доступ к авторизации и регистрации будет заблокирован на 2 минуты");
                        RegPage.IsEnabled = false;
                        Auth.IsEnabled = false;
                        timer.Interval = new TimeSpan(0, 0, 15);
                        timer.Tick += new EventHandler(isVisibleBtn);
                        timer.Start();

                    }

                }
                else
                {
                    AuthUser.user = user;
                    MessageBox.Show("Авторизация выполнена успешно");
                    MainWindow2 window2 = new MainWindow2();

                    window2.Show();
                    MainWindow.window.Close();

                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
           
        }

    

        private void RegPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
