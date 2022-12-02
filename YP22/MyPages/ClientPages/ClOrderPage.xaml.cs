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
        public static List<Product> products = new List<Product>();

        public ClOrderPage()
        {
            InitializeComponent();
            for (int i =0;i < AuthUser.ListOrderBuy.Count; i++)
            {
                DBConnect.ConnectClass.db.OrderProduct.Add(AuthUser.ListOrderBuy[i]);
               
            }
            DBConnect.ConnectClass.db.SaveChanges();

            var sql =( from O in DBConnect.ConnectClass.db.Order
                      where (O.Customer == AuthUser.user.id) && (O.ExecutionStageId == null) /*Находим заказ без статуса*/
                      select O).FirstOrDefault();

            var sql2 = (from Or in ConnectClass.db.OrderProduct /*Отбираем все пррдукты по заказу и соединяем для получения названия*/
                       where (Or.Order == sql)
                       join Pr in ConnectClass.db.Product on Or.ProductId equals Pr.id
                       select Or);
            //sql2 = (IQueryable<OrderProduct>)sql2.ToList();
         
  
            ListProduct.ItemsSource = sql2;
        }
      

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selProduct = (sender as TextBox).DataContext as Product;
          
            
          
        }
    }
}
