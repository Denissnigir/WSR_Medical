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

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += timerTick;
            //dispatcherTimer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            timerCounter += TimeSpan.FromSeconds(1);
            if(timerCounter == new TimeSpan(0, 5, 0))
            {
                ShowMessage.InfMessage("До конца сессии осталось 5 минут!");
            } 
            else if(timerCounter >= new TimeSpan(0, 10, 0))
            {
                dispatcherTimer.Stop();
                NavigationService.Navigate(new SignInPage(true));
            }
        }
    }
}
