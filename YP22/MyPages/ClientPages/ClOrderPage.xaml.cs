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

        public ClOrderPage()
        {
            InitializeComponent();
            Order orderindb = DBConnect.ConnectClass.db.Order.Where(x => x.Customer == AuthUser.user.id && x.ExecutionStageId == null).FirstOrDefault();
            if (orderindb != null)
            {
                AuthUser.order = orderindb;
                List<OrderProduct> IS = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == AuthUser.order.id).ToList();
                IS = IS.Distinct().ToList();
                if (IS != null)
                    ListProduct.ItemsSource = IS;
            }
  
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
            }
            else
                MessageBox.Show("Вы отменили удаление продукта");

            Order orderindb = DBConnect.ConnectClass.db.Order.Where(x => x.Customer == AuthUser.user.id && x.ExecutionStageId == null).FirstOrDefault();
            if (orderindb != null)
            {
                AuthUser.order = orderindb;
                List<OrderProduct> IS = DBConnect.ConnectClass.db.OrderProduct.Where(x => x.OrderId == AuthUser.order.id).ToList();
                IS = IS.Distinct().ToList();
                if (IS != null)
                    ListProduct.ItemsSource = IS;
            }
         
             
        }
    }
}
