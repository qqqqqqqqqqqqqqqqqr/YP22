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
    /// Логика взаимодействия для AdminCountryEditPage.xaml
    /// </summary>
    public partial class AdminCountryEditPage : Page
    {
        SupplierСountry SupplierСountry;
        public AdminCountryEditPage(SupplierСountry supplierСountry)
        {
            InitializeComponent();
            SupplierСountry = supplierСountry;
            DataContext = SupplierСountry;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierСountry.id == 0)
            {
                DBConnect.ConnectClass.db.SupplierСountry.Add(SupplierСountry);
             
            }
            DBConnect.ConnectClass.db.SaveChanges();
            MessageBox.Show("Выполнено!");
            NavigationService.Navigate(new MyPages.AdminPages.AdminCountryPage());

        }
        private void Update()
        {
            if (TbName.Text.Length < 1 )
            {
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel();
                //toolTipPanel.Children.Add(new TextBlock { Text = "Уведомление", FontSize = 16 });
                toolTipPanel.Children.Add(new TextBlock { Text = "Необходимо заполнить поле!" });
                toolTip.Content = toolTipPanel;

                if (TbName.Text.Length < 1)
                {
                    TbName.ToolTip = toolTip;
                }

            }
            else
            {

                BtnSave.IsEnabled = true;
            }
        }
    

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите вернуться назад?", "Уведомление", MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MyPages.AdminPages.AdminCountryPage());

            }

        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
