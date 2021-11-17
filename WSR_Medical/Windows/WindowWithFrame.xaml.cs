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
using System.Windows.Shapes;
using System.Windows.Threading;
using WSR_Medical.Model;
using WSR_Medical.Pages;
using WSR_Medical.Utils;

namespace WSR_Medical.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowWithFrame.xaml
    /// </summary>
    public partial class WindowWithFrame : Window
    {
        DispatcherTimer dispatcherTimer;
        TimeSpan timerCounter;

        public static Employee employee { get; set; }
        public WindowWithFrame()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            MainFrame.Navigate(new SignInPage());
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += timerTick;
            MainFrame.Navigated += checkPage;
        }

        private void checkPage(object sender, EventArgs e)
        {
            if (!(MainFrame.Content is SignInPage) && !(MainFrame.Content is CaptchaPage))
            {
                dispatcherTimer.Start();
            }
            else
            {
                dispatcherTimer.Stop();
            }

            if (MainFrame.Content is CaptchaPage)
            {
                ExitBtn.IsEnabled = false;
            }
            else
            {
                ExitBtn.IsEnabled = true;
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is SignInPage)
            {
                Application.Current.Shutdown();
            }
            else
            {
                WindowWithFrame.employee = null;
                MainFrame.Navigate(new SignInPage());
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            timerCounter += TimeSpan.FromSeconds(1);
            string[] total = timerCounter.ToString().Split(':');
            TimerTB.Text = $"{total[1]}:{total[2]}";
            if (timerCounter == new TimeSpan(0, 5, 10))
            {
                new Thread(() =>
                {
                    ShowMessage.InfMessage("До конца сессии осталось 5 минут!");
                }).Start();
            }
            else if (timerCounter >= new TimeSpan(0, 10, 0))
            {
                timerCounter = TimeSpan.FromSeconds(0);
                dispatcherTimer.Stop();
                MainFrame.Navigate(new SignInPage(true));
            }
        }
    }
}
