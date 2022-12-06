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

        private void Up()
        {
            List<Product> products = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();

            if (CbUnit.SelectedIndex == 1)
            {
                products = products.Where(x => x.UnitID == 4).ToList();
            }
            else if (CbUnit.SelectedIndex == 2)
            {
                products = products.Where(x=> x.UnitID == 1).ToList();
            }
            else if (CbUnit.SelectedIndex == 3)
            {
                products = products.Where(x => x.UnitID == 2).ToList();
            }
            else if (CbUnit.SelectedIndex == 4)
            {
                products = products.Where(x => x.UnitID == 3).ToList();
            }
          
            if(CbSort.SelectedIndex == 1)
            {
                products = products.OrderBy(x => x.Name).ToList();
               
            }
            else if (CbSort.SelectedIndex == 2)
            {
                products = products.OrderByDescending(x => x.Date).ToList();

            }

            if(TbSearch.Text.Length > 0)
            {
     
                products = products.Where(x => ( x.Name != null &&  x.Name.ToLower().StartsWith(TbSearch.Text.ToLower()) ) || ( x.Description != null &&  x.Description.ToLower().StartsWith(TbSearch.Text.ToLower()))).ToList();


            }

            ListProduct.ItemsSource = products.ToList();


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
                ListProduct.ItemsSource = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();

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

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Up();

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();

        }

        private void CbUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();
        }
    }
}
