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

        Timer timer = new Timer();

     
        public void ElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            RegPage.IsEnabled = true;
            Auth.IsEnabled = true;
            count = 0;
       
        }
        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user = DBConnect.ConnectClass.db.User.Where(x => TbLogin.Text.Length > 0 && TbPassword.Text.Length > 0 && (x.LogIn == TbLogin.Text && x.Password.Trim() == TbPassword.Text.Trim())).FirstOrDefault();
            if (user == null)
            {
                if(count < 3)
                {
                    count++;
                    MessageBox.Show("Данные введены неверно.У вас соталось" +(3-count)+ " попытки до блокировки.");
                }
                else if (count == 3)
                {
                    RegPage.IsEnabled = false;
                    Auth.IsEnabled = false;


                    timer.Enabled = true;
                    timer.Interval = 2000;
                    timer.Start();
                    timer.Elapsed += new ElapsedEventHandler(ElapsedEventHandler);
                    
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

    

        private void RegPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
