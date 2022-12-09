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

namespace YP22.MyPages.StorekeeperPages
{
    /// <summary>
    /// Логика взаимодействия для StProduct.xaml
    /// </summary>
    public partial class StProduct : Page
    {
        int MaxCount = 0;
        int RealCount = 0;
        int ActualPages = 0;
        int OneCount = 0;
        public StProduct()
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

        private void CbUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Up();
        }

        private void CbCountVisible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Up();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            if (MessageBox.Show("Вы хотите добавить этот продукт в поставку?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
               if( YP22.Classes.AuthUser.AdmissionProduct == null)
                {
                    AdmissionProduct admissionProduct = new AdmissionProduct();

                    admissionProduct.AcceptanceStatus = 3;

                    DBConnect.ConnectClass.db.AdmissionProduct.Add(admissionProduct);

                    DBConnect.ConnectClass.db.SaveChanges();
                    YP22.Classes.AuthUser.AdmissionProduct = DBConnect.ConnectClass.db.AdmissionProduct.Where(x => x.AcceptanceStatus == 3).FirstOrDefault();
                    Purchase purchase = new Purchase();
                    purchase.AdmissionProductId = YP22.Classes.AuthUser.AdmissionProduct.id;
                    purchase.ProductId = selProduct.id;
                    DBConnect.ConnectClass.db.Purchase.Add(purchase);
                    DBConnect.ConnectClass.db.SaveChanges();
                }
                else
                {
                    Purchase purchase = new Purchase();
                    purchase.AdmissionProductId = YP22.Classes.AuthUser.AdmissionProduct.id;
                    purchase.ProductId = selProduct.id;
                    DBConnect.ConnectClass.db.Purchase.Add(purchase);
                    DBConnect.ConnectClass.db.SaveChanges();
                }
                DBConnect.ConnectClass.db.SaveChanges();
            }
         
        }

        private void ChecedMonth_Click(object sender, RoutedEventArgs e)
        {
            Up();
        }
    }
}
