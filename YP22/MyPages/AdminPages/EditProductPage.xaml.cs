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
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        Product Product;
        public EditProductPage(Product product)
        {
            InitializeComponent();
            
            Update();
            Product = product;
            DataContext = Product;
            

           
        }

        private void Update()
        {
            if (TbName.Text.Length < 1 || TbPrice.Text.Length < 1 || TbDescription.Text.Length < 1 || TbCount.Text.Length < 1)
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
            
            if (TbPrice.Text.Length < 1)
                {
                    TbPrice.ToolTip = toolTip;
                }
             if (TbDescription.Text.Length < 1)
                {
                    TbDescription.ToolTip = toolTip;
                }
             if(TbCount.Text.Length < 1)
                {
                    TbCount.ToolTip = toolTip;
                }

                BtnSave.IsEnabled = false;
            }
            else
            {

                BtnSave.IsEnabled = true;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           
          
           if (Product.id == 0)
            {
                Product.Date = DateTime.Now;
                if (CbUnit.SelectedIndex == 0)
                {
                    Product.UnitID = 4;
                }
                else if(CbUnit.SelectedIndex == 1)
                {
                    Product.UnitID = 1;
                }else if(CbUnit.SelectedIndex == 2)
                {
                    Product.UnitID = 2;
                }else if(CbUnit.SelectedIndex == 3)
                {
                    Product.UnitID = 3;
                }
                DBConnect.ConnectClass.db.Product.Add(Product);
            }
            DBConnect.ConnectClass.db.SaveChanges();
            MessageBox.Show("Выполнено!");
            NavigationService.Navigate(new MyPages.AdminPages.AdminProductPage());
           
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
           
        }

        private void TbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
  
         
        }

        private void TbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите отменить и вернуться назад?", "Уведомление", MessageBoxButton.YesNo) ==
             MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MyPages.AdminPages.AdminProductPage());

            }
           

            
        }
    }
}
