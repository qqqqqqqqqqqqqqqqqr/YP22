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

namespace YP22.MyPages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для ClProductPage.xaml
    /// </summary>
    public partial class ClProductPage : Page
    {
        int MaxCount = 0;
        int RealCount = 0;
        int ActualPages = 0;
        int OneCount = 0;
        public ClProductPage()
        {
            InitializeComponent();
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();
            MaxCount = (DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList()).Count;
            RealCount = MaxCount;
            TxtMaxCount.Text = MaxCount.ToString();
            TxtRealCount.Text = RealCount.ToString(); 

        }

        Order order = new Order();
        private void BtnOrderAdd_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;

            Order orderindb = DBConnect.ConnectClass.db.Order.Where(x => x.Customer == AuthUser.user.id && x.ExecutionStageId == null).FirstOrDefault(); /*проверка на уже созданную корзину, но не оформленную */
            if(orderindb == null)
            {
            
                order.Customer = AuthUser.user.id;
                DBConnect.ConnectClass.db.Order.Add(order);
                DBConnect.ConnectClass.db.SaveChanges();

            }
            else
            {
                order = orderindb;
            }
            AuthUser.order = order;



            OrderProduct orderProductInDb = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == order.id && x.ProductId == selProduct.id).FirstOrDefault();
            if (orderProductInDb == null)
            {
                if (selProduct.Count >= 1)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.OrderId = order.id;
                    orderProduct.ProductId = selProduct.id;
                    orderProduct.Count = 1;
                    DBConnect.ConnectClass.db.OrderProduct.Add(orderProduct);
                    DBConnect.ConnectClass.db.SaveChanges();
                    MessageBox.Show("Товар был добавлен в корзину");
                }
                else
                {
                    MessageBox.Show("Товара нет в наличии");
                }
                
            }
            else
            {
                if (selProduct.Count > orderProductInDb.Count)
                {
                    orderProductInDb.Count++;
                    DBConnect.ConnectClass.db.SaveChanges();
                    MessageBox.Show("+1 шт");
                }
                else
                {
                    MessageBox.Show("Вы не можете добавить еще, так как колиечесво товара в корзине будет превышать остаток");

                }
            }


            DBConnect.ConnectClass.db.SaveChanges();

   
        }

        private void BtnReadSupplier_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;
            List<SupplierProduct> list1 = new List<SupplierProduct>();
            list1 = DBConnect.ConnectClass.db.SupplierProduct.Where(x => x.ProductId == selProduct.id).ToList();
            //List<SupplierСountry> list2 = new List<SupplierСountry>();
            List<string> list3 = new List<string>();
            for (int i = 0; i < list1.Count; i++)
            {
                SupplierСountry supplierСountry = new SupplierСountry();
                int idsup = (int)list1[i].SupplierId;
                supplierСountry = (SupplierСountry) DBConnect.ConnectClass.db.SupplierСountry.Where(x=> x.id == idsup ).FirstOrDefault();
                list3.Add(supplierСountry.Name.ToString());
                //list2.Add(supplierСountry);
            }
            list3 = list3.Distinct().ToList();
            if(list3.Count > 0)
            {
                string text1 = "Поставщиками данного товара являются: ";
                string text2 = " , ";
                for (int i = 0; i < list3.Count; i++)
                {
                    if (i == (list3.Count - 1))
                    {
                        text1 = text1 + list3[i];
                    }
                    else
                        text1 = text1 + list3[i] + text2;
                }
                MessageBox.Show(text1, "О поставщиках");
            }
            else
            MessageBox.Show("На данный момент поставщики данного товара не определены.", "О поставщиках");
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


            if (CbSort!= null && CbSort.SelectedIndex == 1)
            {
                products = products.OrderBy(x => x.Name).ToList();

            }
            else if (CbSort!= null && CbSort.SelectedIndex == 2)
            {
                products = products.OrderByDescending(x => x.Date).ToList();

            }

            if (TbSearch!= null && TbSearch.Text.Length > 0)
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

            if (ListProduct!= null)
            {
                ListProduct.ItemsSource = products;
                RealCount = products.Count;
                TxtRealCount.Text = RealCount.ToString();

            }









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
            if(ActualPages > 0)
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
    }
}
