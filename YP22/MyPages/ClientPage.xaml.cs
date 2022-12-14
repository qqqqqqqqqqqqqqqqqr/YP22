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
using YP22.MyPages.ClientPages;
using YP22.DBConnect;
namespace YP22.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            MyFrames.Navigate(new ClStartPage());
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            
            MyFrames.Navigate(new ClProductPage());
        }

        private void BtnBasket_Click(object sender, RoutedEventArgs e)
        {
            MyFrames.Navigate(new ClOrderPage());
        }

        private void BtnStory_Click(object sender, RoutedEventArgs e)
        {
            MyFrames.Navigate(new MyPages.ClientPages.ClientMyOrdersPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Уведомление", MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                YP22.Classes.AuthUser.user = null;
                YP22.Classes.AuthUser.order = null;

                mainWindow.Show();
                MyWindow.MainWindow2.Window2.Close();

            }
            else
                MessageBox.Show("Вы отменили выход");
        }
    }
}
