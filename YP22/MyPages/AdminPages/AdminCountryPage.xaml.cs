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
    /// Логика взаимодействия для AdminCountryPage.xaml
    /// </summary>
    public partial class AdminCountryPage : Page
    {
        public AdminCountryPage()
        {
            InitializeComponent();
            ListCountry.ItemsSource = DBConnect.ConnectClass.db.SupplierСountry.Where(x => x.isDelete != true).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var sel = (sender as Button).DataContext as SupplierСountry;
            if (MessageBox.Show("Вы хотите отредактировать данную запись?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MyPages.AdminPages.AdminCountryEditPage(sel));

            }
            else
                MessageBox.Show("Вы отменили редактирование");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = (sender as Button).DataContext as SupplierСountry;


            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo) ==
              MessageBoxResult.Yes)
            {
                sel.isDelete = true;
                MessageBox.Show("Вы удалили данный продукт");
                DBConnect.ConnectClass.db.SaveChanges();
                ListCountry.ItemsSource = DBConnect.ConnectClass.db.SupplierСountry.Where(x => x.isDelete != true).ToList();

            }
            else
                MessageBox.Show("Вы отменили удаление продукта");
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
