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

namespace YP22.MyPages.StorekeeperPages
{
    /// <summary>
    /// Логика взаимодействия для AllAdmissPage.xaml
    /// </summary>
    public partial class AllAdmissPage : Page
    {
        public AllAdmissPage()
        {
            InitializeComponent();
            Lv.ItemsSource = DBConnect.ConnectClass.db.AdmissionProduct.Where(x => x.AcceptanceStatus == 1).ToList();
        }
    }
}
