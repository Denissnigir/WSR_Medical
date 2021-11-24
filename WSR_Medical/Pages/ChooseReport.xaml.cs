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

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChooseReport.xaml
    /// </summary>
    public partial class ChooseReport : Page
    {
        public ChooseReport()
        {
            InitializeComponent();
        }

        private void ToQualityControl(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }

        private void ToServiceReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceReport());

        }
    }
}
