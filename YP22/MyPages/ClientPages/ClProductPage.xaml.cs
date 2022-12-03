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

        public  List<ProductBusket> ListBusket = new List<ProductBusket>();
        private void BtnOrderAdd_Click(object sender, RoutedEventArgs e)
        {
            var selProduct = (sender as Button).DataContext as Product;

            ProductBusket productBusket = new ProductBusket();
            productBusket.Product = selProduct;
            productBusket.Count = 1;
            productBusket.MaxCount = (int)productBusket.Product.Count;

            ListBusket.Add(productBusket);
            ListBusket = ListBusket.Distinct().ToList();
            AuthUser.ListBusket = ListBusket;
            

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
    }
}
