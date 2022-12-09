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

namespace YP22.MyPages.StorekeeperPages
    
{
    /// <summary>
    /// Логика взаимодействия для NewProductPage.xaml
    /// </summary>
    public partial class NewProductPage : Page
    {
        AdmissionProduct Admiss;
        public NewProductPage()
        {
            InitializeComponent();
            AuthUser.AdmissionProduct = DBConnect.ConnectClass.db.AdmissionProduct.Where(x => x.AcceptanceStatus == 3).FirstOrDefault();
            Admiss = AuthUser.AdmissionProduct;
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Purchase.Where(x => x.AdmissionProductId == AuthUser.AdmissionProduct.id).ToList();
            //List<SupplierСountry> supplierСountries = DBConnect.ConnectClass.db.SupplierСountry.ToList();
            //for (int i = 0; i < supplierСountries.Count; i++)
            //{
            //    CbCountry.Items.Add(supplierСountries[i]);
            //}



        }
        private void Up()
        {

            //AuthUser.AdmissionProduct = DBConnect.ConnectClass.db.AdmissionProduct.Where(x => x.AcceptanceStatus == 3).FirstOrDefault();
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Purchase.Where(x => x.AdmissionProduct == AuthUser.AdmissionProduct).ToList();
          


        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите очистить эту поставку?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                List<Purchase> purchasesClear = DBConnect.ConnectClass.db.Purchase.Where(x => x.AdmissionProductId == AuthUser.AdmissionProduct.id).ToList();
                for(int i =0; i < purchasesClear.Count; i++)
                {
                    DBConnect.ConnectClass.db.Purchase.Remove(purchasesClear[i]);
                 }
                DBConnect.ConnectClass.db.SaveChanges();
                MessageBox.Show("Очищено");
            }
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите оформить эту поставку?", "Уведомление", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                double AdmissPrice = 0;
                int AdmissCount = 0;
                List<Purchase> purchasesClear = DBConnect.ConnectClass.db.Purchase.Where(x => x.AdmissionProductId == AuthUser.AdmissionProduct.id).ToList();
                for (int i = 0; i < purchasesClear.Count; i++)
                {
                    if(purchasesClear[i].Count > 0 && purchasesClear[i].Price > 0)
                    {
                        AdmissCount += (int)purchasesClear[i].Count;
                        AdmissPrice += (int)(purchasesClear[i].Count * purchasesClear[i].Price);
                        purchasesClear[i].Product.Count += (int)purchasesClear[i].Count;
                    }
                    else
                    {
                        
                        DBConnect.ConnectClass.db.Purchase.Remove(purchasesClear[i]);
                    }
                   
                }
                Admiss.Count = AdmissCount;
                Admiss.SummPrice = (decimal)AdmissPrice;
                Admiss.AcceptanceStatus = 1;
                Admiss.Date = DateTime.Now;
                AuthUser.AdmissionProduct = null;
                DBConnect.ConnectClass.db.SaveChanges();
                MessageBox.Show("Оформлено. Товары добавлены в наличие Если вы не вписали количество или закупочную стоимость, то этот товар автоматически удалился из поставки при оформлении");
                Up();
            }
        }

       
        private void CbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
