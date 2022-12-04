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
        public ClProductPage()
        {
            InitializeComponent();
            ListProduct.ItemsSource = DBConnect.ConnectClass.db.Product.Where(x => x.IsDelete != true).ToList();

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

         


            //создаем в ьд ордер и на него все остальное. В коризне выводим уже заполненный ордерПродукт . Если корзина очищается
            //без оформления заказа, то просто удалить все данные на этот ордер
            //Отличить этот ордер можно по null статусу 


            // OrderProduct orderProductInDb = new OrderProduct();
            //if (( orderProductInDb = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == orderProduct.OrderId && x.ProductId == orderProduct.ProductId).FirstOrDefault()) == null)
            // {
            //     orderProduct.Count = 1;
            //     DBConnect.ConnectClass.db.OrderProduct.Add(orderProduct);
            // }
            //else
            // {
            //     if(orderProductInDb.Product.Count < (orderProductInDb.Count += 1))
            //     {
            //         MessageBox.Show("");
            //     }


            // }



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
    }
}
