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
  
        public ClOrderPage( )
        {
            InitializeComponent();
        
                ItemSourceList();
            
          
  
        }

      

        private void ItemSourceList()
        {
            Order orderindb = DBConnect.ConnectClass.db.Order.Where(x => x.Customer == AuthUser.user.id && x.ExecutionStageId == null).FirstOrDefault();
            if (orderindb != null)
            {
                AuthUser.order = orderindb;
                List<OrderProduct> IS = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == AuthUser.order.id).ToList();
                IS = IS.Distinct().ToList();
                if (IS != null)
                    ListProduct.ItemsSource = IS;

                int count = 0;
                double summ = 0;
                for (int i = 0; i < IS.Count; i++)
                {

                    count += (int)IS[i].Count;
                    summ += (double) (IS[i].Product.Price * IS[i].Count);

                }
                orderindb.CountSumm = count;
                orderindb.PriceSumm = (decimal)summ;
                TxtCountProduct.Text = count.ToString();
                TxtSumm.Text = summ.ToString();
                DBConnect.ConnectClass.db.SaveChanges();
            }
            else
                ListProduct.ItemsSource = null;
        }
        
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы точно хотите удалить все товары из корзины?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                do
                {
                    OrderProduct OPDelete = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == AuthUser.order.id).FirstOrDefault();
                    if (OPDelete != null)
                        DBConnect.ConnectClass.db.OrderProduct.Remove(OPDelete);
                    DBConnect.ConnectClass.db.SaveChanges();
                }
                while ((DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == AuthUser.order.id).FirstOrDefault()) != null);
                ItemSourceList();
            }
            else
                MessageBox.Show("Вы отменили очистку корзины");

            ItemSourceList();



        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите оформить этот заказ?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                Order order = DBConnect.ConnectClass.db.Order.Where(x => x.id == AuthUser.order.id).FirstOrDefault();
                order.Data = DateTime.Now;
                order.ExecutionStageId = 1;                                /*Нужно сделать вычитание продуктов из наличия*/
               
                List<OrderProduct> products = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == order.id).ToList();  /*Собираем все в лист, чтобы убрать количество продуктов из наличия*/
                for ( int i = 0; i<products.Count;i++)
                {
                    products[i].Product.Count = products[i].Product.Count - products[i].Count;
               
                }
                DBConnect.ConnectClass.db.SaveChanges();
                AuthUser.order = null;
                ItemSourceList();
                TxtCountProduct.Text = "0";
                TxtSumm.Text = "0";


                MessageBox.Show("Заказ принят!");
            }
            else
                MessageBox.Show("Вы отменили оформление заказа");

            
        }
    }
}
