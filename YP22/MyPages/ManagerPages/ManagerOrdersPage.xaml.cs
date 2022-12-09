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

namespace YP22.MyPages.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для ManagerOrdersPage.xaml
    /// </summary>
    public partial class ManagerOrdersPage : Page
    {
        public ManagerOrdersPage()
        {
            InitializeComponent();
            Up();
            


        }
        List <Order> orders;
        private void Up()
        {

            orders = DBConnect.ConnectClass.db.Order.Where(x => x.Executor == YP22.Classes.AuthUser.user.id || x.Executor == null).ToList();

            if (RB2.IsChecked == true)
            {
                orders = orders.Where(x => x.Executor == YP22.Classes.AuthUser.user.id).ToList();
            }
            else if (RB3.IsChecked == true)
            {
                orders = orders.Where(x => x.Executor == null && x.ExecutionStageId != 10).ToList();
            }
            ListMyOrders.ItemsSource = orders;
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите принять этот заказ?", "Уведомление", MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
            {
                var sel = (sender as Button).DataContext as Order;
                sel.Executor = YP22.Classes.AuthUser.user.id;
                sel.ExecutionStageId = 2;
                DBConnect.ConnectClass.db.SaveChanges();
                MessageBox.Show("Вы назначены исполнителем данного заказа!");
                Up();
            }
           
        }

        private void BtnExt_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Желаете изменить статус заказа?", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
               Order sel = (sender as Button).DataContext as Order;

                NavigationService.Navigate(new MyPages.ManagerPages.ManagerEditStatePage(sel));
            }
        }

        private void RB1_Click(object sender, RoutedEventArgs e)
        {
            Up();
        }

        private void BtnState1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отклонить заказ?", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
                Order sel = (sender as Button).DataContext as Order;
                sel.ExecutionStageId = 3;
                DBConnect.ConnectClass.db.SaveChanges();
                Up();
            }

           
        }

        private void BtnState2_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Направить заказ к оплате?", "Уведомление", MessageBoxButton.YesNo) ==
          MessageBoxResult.Yes)
            {
                Order sel = (sender as Button).DataContext as Order;
                sel.ExecutionStageId = 4;
                DBConnect.ConnectClass.db.SaveChanges();
                Up();
            }
        }

        private void BtnState3_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Направить на выполнение?", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
                Order sel = (sender as Button).DataContext as Order;
                sel.ExecutionStageId = 6;
                DBConnect.ConnectClass.db.SaveChanges();
                Up();
            }
        }

        //private void BtnState4_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MessageBox.Show("Отметить как оплаченный?", "Уведомление", MessageBoxButton.YesNo) ==
        //   MessageBoxResult.Yes)
        //    {
        //        Order sel = (sender as Button).DataContext as Order;
        //        sel.ExecutionStageId = 5;
        //        DBConnect.ConnectClass.db.SaveChanges();
        //        Up();
        //    }
        //}

        private void BtnState5_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отметить как готовый?", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
                Order sel = (sender as Button).DataContext as Order;
                sel.ExecutionStageId = 7;
                DBConnect.ConnectClass.db.SaveChanges();
                Up();
            }
        }
    }
}
