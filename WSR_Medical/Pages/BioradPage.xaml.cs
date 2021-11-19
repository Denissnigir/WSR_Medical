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
    /// Логика взаимодействия для BioradPage.xaml
    /// </summary>
    public partial class BioradPage : Page
    {
        public BioradPage()
        {
            InitializeComponent();
            ServiceList.ItemsSource = Context._con.OrderService.Where(p => p.StatusId == 1 && p.Service.AnalyzerService.FirstOrDefault().AnalyzerId == 2).ToList();
        }
    }
}
