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

            Add.Visibility = YP22.Classes.AuthUser.Visibility;


        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            productBoll = true;
            userBoll = false;
            ordersBool = false;
            countryBool = false;
            MyFrame.Navigate(new YP22.MyPages.AdminPages.AdminProductPage());
            
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            productBoll = false;
            userBoll = false;
            countryBool = false;
            ordersBool = true;
        }

        private void BtnCountry_Click(object sender, RoutedEventArgs e)
        {
            productBoll = false;
            userBoll = false;
            ordersBool = false;
        
            countryBool = true;
            MyFrame.Navigate(new MyPages.AdminPages.AdminCountryPage());
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            productBoll = false;

            ordersBool = false;
            countryBool = false;
            userBoll = true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (productBoll == true)
            {
                productBoll = false;
                MyFrame.Navigate(new MyPages.AdminPages.EditProductPage(new Product()));
            }
            else if (ordersBool == true)
            {
                ordersBool = false;
            }
            else if (countryBool == true)
            {
                countryBool = false;
                MyFrame.Navigate(new MyPages.AdminPages.AdminCountryEditPage(new SupplierСountry()));
            }
            else if (userBoll == true)
            {
                userBoll = false;
            }
        }
    }
}
