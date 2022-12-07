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
    /// Логика взаимодействия для ManagerEditStatePage.xaml
    /// </summary>
    public partial class ManagerEditStatePage : Page
    {
        Order Order;
        public ManagerEditStatePage(Order order )
        {
            InitializeComponent();
            Order = order;
            DataContext = Order;

             //Отклонен </ ComboBoxItem > 0
             // К оплате </ ComboBoxItem > 1

              //Оплачен </ ComboBoxItem > 2

              //Выполнение </ ComboBoxItem > 3

              //Готов </ ComboBoxItem > 4
              //В обработке </ ComboBoxItem > 5

            if(Order.ExecutionStageId == 2)
            {
                CbState.SelectedIndex = 5;
            }
            else if (Order.ExecutionStageId == 3)
            {
                CbState.SelectedIndex = 0;

            }
            else if (Order.ExecutionStageId == 4)
            {
                CbState.SelectedIndex = 1;

            }
            else if (Order.ExecutionStageId == 5)
            {
                CbState.SelectedIndex = 2;

            }
            else if (Order.ExecutionStageId == 6)
            {
                CbState.SelectedIndex = 3;

            }
            else if (Order.ExecutionStageId == 7)
            {
                CbState.SelectedIndex = 4;

            }
           


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Желаете сохранить?", "Уведомление", MessageBoxButton.YesNo) ==
           MessageBoxResult.Yes)
            {
                if (CbState.SelectedIndex == 5)
                {
                    Order.ExecutionStageId = 2;
                }
                else if (CbState.SelectedIndex == 0)
                {

                    Order.ExecutionStageId = 3;
                }
                else if (CbState.SelectedIndex == 1)
                {
                    Order.ExecutionStageId = 4;

                }
                else if (CbState.SelectedIndex == 2)
                {
                    Order.ExecutionStageId = 5;

                }
                else if (CbState.SelectedIndex == 3)
                {
                    Order.ExecutionStageId = 6;

                }
                else if (CbState.SelectedIndex == 4)
                {
                    Order.ExecutionStageId = 7;

                }

                DBConnect.ConnectClass.db.SaveChanges();


                if (MessageBox.Show("Сохранено. Вернуться назад?", "Уведомление", MessageBoxButton.YesNo) ==
                         MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new MyPages.ManagerPages.ManagerOrdersPage());
                }

                {
                }
            }
            


            
        }

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(" Изменения не будут сохранены. Вернуться назад?", "Уведомление", MessageBoxButton.YesNo) ==
                         MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MyPages.ManagerPages.ManagerOrdersPage());
            }

        }
    }
}
