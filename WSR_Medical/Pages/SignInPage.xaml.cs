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
using WSR_Medical.Utils;
using System.Windows.Threading;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        DispatcherTimer dispatcherTimer;
        TimeSpan timerCounter;
        public SignInPage(bool isLocked = false)
        {
            InitializeComponent();
            SignInBtn.IsEnabled = !isLocked;
            if (isLocked)
            {
                ShowMessage.InfMessage("Вход заблокирован на 1 минуту!");
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += timerTick;
                dispatcherTimer.Start();
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            timerCounter += TimeSpan.FromSeconds(1);
            if(timerCounter >= new TimeSpan(0, 1, 0))
            {
                ShowMessage.InfMessage("Теперь вы можете войти!");
                dispatcherTimer.Stop();
                NavigationService.Navigate(new SignInPage(false));
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)ShowPasswordCB.IsChecked)
                {
                    if (!string.IsNullOrWhiteSpace(LoginTB.Text) && !string.IsNullOrWhiteSpace(PasswordTB.Text))
                    {
                        WindowWithFrame.employee = Context._con.Employee.Where(p => p.Login == LoginTB.Text && p.Password == PasswordTB.Text).FirstOrDefault();
                    }
                    else
                    {
                        ShowMessage.ErrMessage("Введите данные!");
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(LoginTB.Text) && !string.IsNullOrWhiteSpace(PasswordPB.Password))
                    {
                        WindowWithFrame.employee = Context._con.Employee.Where(p => p.Login == LoginTB.Text && p.Password == PasswordPB.Password).FirstOrDefault();
                    }
                    else
                    {
                        ShowMessage.ErrMessage("Введите данные!");
                    }
                }

            }
            catch
            {
                ShowMessage.ErrMessage("Что-то пошло не так...");
            }



            if (WindowWithFrame.employee != null)
            {
                NavigationService.Navigate(new AdminPage());
            }
            else
            {
                ShowMessage.ErrMessage("Неправильный логин или пароль!");
                NavigationService.Navigate(new CaptchaPage());
            }
        }

        private void ShowPasswordCB_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTB.Visibility = Visibility.Visible;
            PasswordTB.Text = PasswordPB.Password;
            PasswordPB.Visibility = Visibility.Hidden;
        }

        private void ShowPasswordCB_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordPB.Visibility = Visibility.Visible;
            PasswordPB.Password = PasswordTB.Text;
            PasswordTB.Visibility = Visibility.Hidden;
        }
    }
}
