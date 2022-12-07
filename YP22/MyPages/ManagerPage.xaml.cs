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

namespace YP22.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
   
     

        public ManagerPage()
        {
            InitializeComponent();
            MyFrame.Navigate(new MyPages.ManagerPages.ManagerOrdersPage());

    }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти из аккаунта?", "Уведомление", MessageBoxButton.YesNo) ==
            MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                YP22.Classes.AuthUser.user = null;
                YP22.Classes.AuthUser.order = null;

                mainWindow.Show();
                MyWindow.MainWindow2.Window2.Close();

            }
            else
                MessageBox.Show("Вы отменили выход");
        }
    }
}
