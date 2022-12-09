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

namespace YP22.MyPages
{
    /// <summary>
    /// Логика взаимодействия для StorekeeperPage.xaml
    /// </summary>
    public partial class StorekeeperPage : Page
    {
        public StorekeeperPage()
        {
            InitializeComponent();
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new MyPages.StorekeeperPages.StProduct());
        }

        private void BtnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new MyPages.StorekeeperPages.NewProductPage());

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

  

        private void BtnAllAdmiss_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new MyPages.StorekeeperPages.AllAdmissPage());
        }
    }
}
