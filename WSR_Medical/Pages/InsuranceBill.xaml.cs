using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для InsuranceBill.xaml
    /// </summary>
    public partial class InsuranceBill : Page
    {
        public InsuranceBill()
        {
            InitializeComponent();
            BillList.ItemsSource = Context._con.Biomaterial.ToList();
            totalPrice.Text = Context._con.Biomaterial.ToList().Sum(i=>i.GetTotalPrice).ToString();
        }

        private void PrintToCsv(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "info.csv";
            saveFileDialog.Filter = ".csv | *.csv ";
            var biomaterials = Context._con.Biomaterial.ToList().Select(p => $"{p.Patient.InsuranceCompany.Name};{p.Patient.GetName};{p.GetServices};{p.GetPrice}").ToList();
            biomaterials.Add(Context._con.Biomaterial.ToList().Sum(i => i.GetTotalPrice).ToString());
            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllLines(saveFileDialog.FileName, biomaterials, Encoding.Unicode);
            }
        }

        private void PrintToPdf(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PdfPrintPage());
        }
    }
}
