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
    /// Логика взаимодействия для AnalyzerPage.xaml
    /// </summary>
    public partial class AnalyzerPage : Page
    {
        public AnalyzerPage()
        {
            InitializeComponent();
        }

        private void ToLedetect(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LedetectPage());
        }

        private void ToBiorad(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BioradPage());
        }
    }
}