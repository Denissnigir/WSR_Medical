using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WSR_Medical.Utils;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {
        public PrintPage(Biomaterial biomaterial, int price)
        {
            InitializeComponent();
            MainGrid.DataContext = biomaterial;
            DateTB.Text = Convert.ToString(DateTime.Now);
            PriceTB.Text = Convert.ToString(price);
            BirthDateTB.Text = Convert.ToString(DateTime.Now);

            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog())
            {
                printDialog.PrintVisual((Visual)MainGrid, "desc");
            }


            ShowMessage.InfMessage("Успешно добавлено!");
            NavigationService.Navigate(new LaborantPage());
        }
    }
}
