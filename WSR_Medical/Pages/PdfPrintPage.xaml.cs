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
    /// Логика взаимодействия для PdfPrintPage.xaml
    /// </summary>
    public partial class PdfPrintPage : Page
    {
        public PdfPrintPage()
        {
            InitializeComponent();
            BillList.ItemsSource = Context._con.Biomaterial.ToList();
            totalPrice.Text = Context._con.Biomaterial.ToList().Sum(i => i.GetTotalPrice).ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog())
            {
                printDialog.PrintVisual(PrintPanel, "desc");
            }
            NavigationService.Navigate(new InsuranceBill());
        }
    }
}
