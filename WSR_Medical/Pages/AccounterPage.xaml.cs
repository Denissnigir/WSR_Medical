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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccounterPage.xaml
    /// </summary>
    public partial class AccounterPage : Page
    {
        public AccounterPage()
        {
            InitializeComponent();
        }

        private void CreateInsuranceBill(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InsuranceBill());
        }
    }
}
