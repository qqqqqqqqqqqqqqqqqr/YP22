﻿using System;
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
using System.Windows.Shapes;
using YP22.Classes;
using YP22.MyPages;

namespace YP22.MyWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        public MainWindow2()
        {
            InitializeComponent();
            if (AuthUser.user.RoleId == 1)
            {
                MyFrame.Navigate(new ClientPage()) ;
            }
            else if (AuthUser.user.RoleId == 2)
            {
                MyFrame.Navigate(new AdminPage());
            }
        }
    }
}
