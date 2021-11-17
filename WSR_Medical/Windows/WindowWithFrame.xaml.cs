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
using System.Windows.Shapes;
using WSR_Medical.Model;
using WSR_Medical.Pages;

namespace WSR_Medical.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowWithFrame.xaml
    /// </summary>
    public partial class WindowWithFrame : Window
    {
        public static Employee employee { get; set; }
        public WindowWithFrame()
        {
            InitializeComponent();
            MainFrame.Navigate(new SignInPage());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if(MainFrame.Content is SignInPage)
            {
                Application.Current.Shutdown();
            }
            else
            {
                MainFrame.Navigate(new SignInPage());
            }
        }
    }
}
