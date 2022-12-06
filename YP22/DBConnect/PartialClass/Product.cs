using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YP22.DBConnect
{
   public partial class Product
    {
        public Visibility visibilityCount
        {
            get
            {
                if (Count == 0)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Hidden;
            }
        }

        public Visibility visibleBtn
        {
            get
            {
                if (Count == 0)
                {
                  
                    return Visibility.Hidden;
                }
                else
                    return Visibility.Visible;
            }
        }

        public int borderTich
        {
            get
            {
                if (Count == 0)
                {
                    return 3;
                }
                else
                    return 0;
            }
        }

    }
}
