using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YP22.DBConnect;

namespace YP22.Classes
{
    public class AuthUser
    {
        public static User user = new User();
        public static List<ProductBusket> ListBusket = new List<ProductBusket>();

        public static Visibility Visibility = Visibility.Visible;
        public static Order order;
    }
}
