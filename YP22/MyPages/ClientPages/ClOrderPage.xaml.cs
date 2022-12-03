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
using YP22.Classes;
using YP22.DBConnect;
using YP22.MyPages;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Collections;

namespace YP22.MyPages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для ClOrderPage.xaml
    /// </summary>
    public partial class ClOrderPage : Page
    {


        public List<ProductBusket> productBuskets = new List<ProductBusket>();
        public ClOrderPage()
        {
            InitializeComponent();
            productBuskets = AuthUser.ListBusket;
            productBuskets = productBuskets.Distinct().ToList();
            
            ListProduct.ItemsSource = productBuskets;
        }
      

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selProduct = (sender as TextBox).DataContext as Product;
          
            
          
        }
    }
}
