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
using WSR_Medical.Utils;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : Page
    {
        Random rnd = new Random();
        public CaptchaPage()
        {
            InitializeComponent();
            CaptchaBtn.Content = Convert.ToString(rnd.Next(1000, 9999));
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
            }
            else
            {
                
            }
        }
    }
}
