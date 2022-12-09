using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            CbCountry.ItemsSource = DBConnect.ConnectClass.db.SupplierСountry.ToList();
            



            CbCountry.DisplayMemberPath = "Name";
        }

       

        private void Update()
        {
           if(Product != null)
            {
                List<SupplierProduct> supplierProducts = DBConnect.ConnectClass.db.SupplierProduct.Where(x => x.ProductId == Product.id).ToList();
                List<SupplierСountry> supplierСountries = new List<SupplierСountry>();
                SupplierСountry supplier;
                for (int i = 0; i < supplierProducts.Count; i++)
                {
                    supplier = supplierProducts[i].SupplierСountry;
                    supplierСountries.Add(supplier);
                }
                LvCountry.ItemsSource = supplierСountries.ToList();
            }
           

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
            else
            {
                if (CbUnit.SelectedIndex == 0)
                {
                    Product.UnitID = 4;
                }
                else if (CbUnit.SelectedIndex == 1)
                {
                    Product.UnitID = 1;
                }
                else if (CbUnit.SelectedIndex == 2)
                {
                    Product.UnitID = 2;
                }
                else if (CbUnit.SelectedIndex == 3)
                {
                    Product.UnitID = 3;
                }
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = (sender as Button).DataContext as SupplierСountry;
            if(sel != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить этого поставщика?", "Уведомление", MessageBoxButton.YesNo) ==
         MessageBoxResult.Yes)
                {
                    SupplierProduct supplierProduct = DBConnect.ConnectClass.db.SupplierProduct.Where(x=> x.ProductId == Product.id && x.SupplierId == sel.id).FirstOrDefault();
                    DBConnect.ConnectClass.db.SupplierProduct.Remove(supplierProduct);
                    DBConnect.ConnectClass.db.SaveChanges();
                    Update();
                }
             
            }
            else
            {
                MessageBox.Show("Ничего не выбрано");
            }
        

        }

        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            if(CbCountry.SelectedIndex > -1)
            {
                if (MessageBox.Show("Вы точно хотите добавить этого поставщика?", "Уведомление", MessageBoxButton.YesNo) ==
         MessageBoxResult.Yes)
                {
                    var sel = CbCountry.SelectedItem as SupplierСountry;
                    SupplierProduct supplierProduct = new SupplierProduct();
                    supplierProduct.SupplierId = sel.id;
                    supplierProduct.ProductId = Product.id;
                    DBConnect.ConnectClass.db.SupplierProduct.Add(supplierProduct);
                    DBConnect.ConnectClass.db.SaveChanges();
                    Update();
                }

              
                
            }
            else
            {
                MessageBox.Show("Ничего не выбрано");
            }
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg" /*сначала наименование в проводнике потом сам формат*/
            };
            //string name;
  
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                Product.Image  = File.ReadAllBytes(openFile.FileName);
                Images.Source = new BitmapImage(new Uri(openFile.FileName));

                //name = openFile.FileName; /* записываем в бд выбранное изображение в байты*/
                //FileInfo fileInf = new FileInfo(name);
                //string path = "\\Resources\\" + fileInf.Name;
                //FileInfo fileInfo2 = new FileInfo(path);
                
                //string path1 = "C:\\Users\\MSSI\\source\\repos\\YP22\\YP22\\Resourse\\";

                //fileInf.CopyTo("C:\\Users\\MSSI\\source\\repos\\YP22\\YP22\\Resourse\\", true);
              
                //fileInf.CopyTo(path);
                //Product.Image = fileInf.Name;
                //DBConnect.ConnectClass.db.SaveChanges();
                //Update();
            }
        }
    }
}
