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

namespace YP22.MyPages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для ClientMyOrdersPage.xaml
    /// </summary>
    public partial class ClientMyOrdersPage : Page
    {
        public ClientMyOrdersPage()
        {
            InitializeComponent();
            ItemSourse();
        }
        private void ItemSourse()
        {
            ListMyOrders.ItemsSource = DBConnect.ConnectClass.db.Order.Where(x => x.Customer == Classes.AuthUser.user.id).ToList();
            
        }

        private void BtnExt_Click(object sender, RoutedEventArgs e)
        {
            var sel = (sender as Button).DataContext as Order;
            if (MessageBox.Show("Вы точно хотите отменить этот заказ?", "Уведомление", MessageBoxButton.YesNo) ==
             MessageBoxResult.Yes)
            {
                sel.ExecutionStageId = 10;
                DBConnect.ConnectClass.db.SaveChanges();
                ItemSourse();
            }
            

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
        
            if (MessageBox.Show("Вы точно хотите изменить этот заказ? Этот заказ переместится в корзину, куда вы снова сможете добавить товары. Если в корзине уже имеются" +
                "товары, то они пропадут.", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
                Order sel = (sender as Button).DataContext as Order;
             
            }
        }
    }
}
