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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public bool productBoll = true;
        public bool userBoll = false;
        public bool ordersBool = false;
        public bool countryBool = false;
        public AdminPage()
        {
            InitializeComponent();
            MyFrame.Navigate(new YP22.MyPages.AdminPages.AdminProductPage());
            //productBoll = false;
            userBoll = false;
            ordersBool = false;
            countryBool = false;

        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            productBoll = true;
            NavigationService.Navigate(new YP22.MyPages.AdminPages.AdminProductPage());
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            ordersBool = true;
        }

        private void BtnCountry_Click(object sender, RoutedEventArgs e)
        {
            countryBool = true;
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            userBoll = true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (productBoll == true)
            {
                NavigationService.Navigate(new YP22.MyPages.AdminPages.EditProductPage());
            }
            else if (ordersBool == true)
            {

            }
            else if (countryBool == true)
            {

            }
            else if (userBoll == true)
            {

            }
        }
    }
}
