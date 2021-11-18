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
using WSR_Medical.Model;
using System.IO;
using System.Windows.Threading;
using WSR_Medical.Utils;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        DispatcherTimer dispatcherTimer;
        TimeSpan timerCounter;

        public AdminPage()
        {
            InitializeComponent();
            MainGrid.DataContext = WindowWithFrame.employee;

        }


        private void ToSignInHistory(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignInHistory());
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Barcode());
        }
    }
}
