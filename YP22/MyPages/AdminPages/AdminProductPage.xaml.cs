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
        int MaxCount = 0;
        int RealCount = 0;
        int ActualPages = 0;
        int OneCount = 0;
        public AdminProductPage()
        {
            InitializeComponent();

   
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();
            MaxCount = (DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList()).Count;
            RealCount = MaxCount;
            TxtMaxCount.Text = MaxCount.ToString();
            TxtRealCount.Text = RealCount.ToString();

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
                products = products.Where(x => x.UnitID == 1).ToList();
            }
            else if (CbUnit.SelectedIndex == 3)
            {
                products = products.Where(x => x.UnitID == 2).ToList();
            }
            else if (CbUnit.SelectedIndex == 4)
            {
                products = products.Where(x => x.UnitID == 3).ToList();
            }


            if (CbSort != null && CbSort.SelectedIndex == 1)
            {
                products = products.OrderBy(x => x.Name).ToList();

            }
            else if (CbSort != null && CbSort.SelectedIndex == 2)
            {
                products = products.OrderByDescending(x => x.Date).ToList();

            }

            if (TbSearch != null && TbSearch.Text.Length > 0)
            {
                products = products.Where(x => (x.Name != null && x.Name.ToLower().StartsWith(TbSearch.Text.ToLower())) || (x.Description != null && x.Description.ToLower().StartsWith(TbSearch.Text.ToLower()))).ToList();
            }

            if (CbCountVisible != null && CbCountVisible.SelectedIndex == 1)
            {
                products = products.Skip(ActualPages * 10).Take(10).ToList();
                OneCount = 10;

            }
            else if (CbCountVisible != null && CbCountVisible.SelectedIndex == 2)
            {
                products = products.Skip(ActualPages * 50).Take(50).ToList();
                OneCount = 50;


            }
            else if (CbCountVisible != null && CbCountVisible.SelectedIndex == 3)
            {
                products = products.Skip(ActualPages * 200).Take(200).ToList();
                OneCount = 200;


            }

            if (ChecedMonth != null && ChecedMonth.IsChecked == true)
            {
                DateTime date = DateTime.Now;

                products = products.Where(x => x.Date >= new DateTime(date.Year, date.Month, 1) && x.Date <= new DateTime(date.Year, date.Month, 31)).ToList();
            }

            if (ListProduct != null)
            {
                ListProduct.ItemsSource = products;
                RealCount = products.Count;
                TxtRealCount.Text = RealCount.ToString();
            }
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

        private void CbCountVisible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (ActualPages > 0)
            {
                ActualPages--;
                Up();
            }

        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if (RealCount >= OneCount)
            {
                ActualPages++;
                Up();
            }



        }

        private void ChecedMonth_Click(object sender, RoutedEventArgs e)
        {
            Up();
        }
    }
}
