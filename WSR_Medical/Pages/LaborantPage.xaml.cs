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
using WSR_Medical.Windows;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для LaborantPage.xaml
    /// </summary>
    public partial class LaborantPage : Page
    {
        public LaborantPage()
        {
            InitializeComponent();
            MainGrid.DataContext = WindowWithFrame.employee;
        }

        private void TakeBiomaterial(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBiomaterial());
        }

        private void ToReports(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }
    }
}
