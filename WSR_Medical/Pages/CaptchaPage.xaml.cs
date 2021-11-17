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
using System.Windows.Threading;
using WSR_Medical.Utils;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : Page
    {
        Random rnd = new Random();
        DispatcherTimer dispatcherTimer;
        TimeSpan timerCounter;
        public CaptchaPage()
        {
            InitializeComponent();
            CaptchaBtn.Content = Convert.ToString(rnd.Next(1000, 9999));
            dispatcherTimer = new DispatcherTimer();
        }

        private void ChangeCaptcha(object sender, RoutedEventArgs e)
        {
            CaptchaBtn.Content = Convert.ToString(rnd.Next(1000, 9999));
        }

        private void ValidateCaptcha(object sender, RoutedEventArgs e)
        {
            if(CaptchaTB.Text == (string)CaptchaBtn.Content)
            {
                ShowMessage.InfMessage("Вы правильно ввели капчу!");
                NavigationService.Navigate(new SignInPage());
            }
            else
            {
                new Thread(() => { ShowMessage.ErrMessage("Вы заблокированы на 10 секунд!"); }).Start();
                SubmitBtn.IsEnabled = false;
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += timerTick;
                dispatcherTimer.Start();
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            timerCounter += TimeSpan.FromSeconds(1);
            string[] total = timerCounter.ToString().Split(':');
            TimerTB.Text = $"{total[1]}:{total[2]}";
            if (timerCounter >= new TimeSpan(0, 0, 10))
            {
                ShowMessage.InfMessage("Теперь вы можете снова попробовать!");
                SubmitBtn.IsEnabled = true;
                timerCounter = TimeSpan.FromSeconds(0);
                dispatcherTimer.Stop();
                
            }
        }
    }
}
