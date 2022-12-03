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

namespace YP22.MyPages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminProductPage.xaml
    /// </summary>
    public partial class AdminProductPage : Page
    {
        public AdminProductPage()
        {
            InitializeComponent();
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
          

            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                selProduct.IsDelete = true;
                MessageBox.Show("Вы удалили данный продукт");
                DBConnect.ConnectClass.db.SaveChanges();
            }
            else
                MessageBox.Show("Вы отменили удаление продукта");
     
           
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            if (MessageBox.Show("Вы хотите отредактировать данный продукт?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MyPages.AdminPages.EditProductPage(selProduct));
            }
            else
                MessageBox.Show("Вы отменили редактирование");

        }
    }
}
