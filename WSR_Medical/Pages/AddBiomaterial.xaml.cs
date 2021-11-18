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
using WSR_Medical.Model;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBiomaterial.xaml
    /// </summary>
    public partial class AddBiomaterial : Page
    {
        public AddBiomaterial()
        {
            InitializeComponent();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatient());
        }

        private void CheckBiomaterialNumber(object sender, RoutedEventArgs e)
        {
            var biomat = Context._con.Biomaterial.Where(p => p.Barcode == BarcodeName.Text).FirstOrDefault();
            if(biomat == null)
            {
                PatientGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
