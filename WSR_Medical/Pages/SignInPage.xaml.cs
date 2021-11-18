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
using System.Threading;

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
            LoginTB.Focus();
            SignInBtn.IsEnabled = !isLocked;
            if (isLocked)
            {
                new Thread(() => {
                    ShowMessage.InfMessage("Вход заблокирован на 1 минуту!");
                }).Start();
                dispatcherTimer = new DispatcherTimer();
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
            if (timerCounter >= new TimeSpan(0, 1, 0))
            {
                ShowMessage.InfMessage("Теперь вы можете войти!");
                timerCounter = TimeSpan.FromSeconds(0);
                dispatcherTimer.Stop();
                NavigationService.Navigate(new SignInPage(false));
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            EmployeeEnterArchive employeeEnterArchive = new EmployeeEnterArchive();
            try
            {
                if ((bool)ShowPasswordCB.IsChecked)
                {
                    if (!string.IsNullOrWhiteSpace(LoginTB.Text) && !string.IsNullOrWhiteSpace(PasswordTB.Text))
                    {
                        WindowWithFrame.employee = Context._con.Employee.Where(p => p.Login == LoginTB.Text && p.Password == PasswordTB.Text).FirstOrDefault();
                        if (WindowWithFrame.employee != null)
                        {
                            employeeEnterArchive.Date = DateTime.Now;
                            employeeEnterArchive.EmployeeId = WindowWithFrame.employee.Id;
                            employeeEnterArchive.isSuccessfull = true;
                            employeeEnterArchive.Login = LoginTB.Text;
                            if(WindowWithFrame.employee.RoleId == 1)
                            {
                                NavigationService.Navigate(new LaborantPage());
                            } 
                            else if(WindowWithFrame.employee.RoleId == 2)
                            {
                                NavigationService.Navigate(new LaborantIsslPage());
                            }
                            else if (WindowWithFrame.employee.RoleId == 3)
                            {
                                NavigationService.Navigate(new AdminPage());
                            }
                            else if (WindowWithFrame.employee.RoleId == 4)
                            {
                                NavigationService.Navigate(new AccounterPage());
                            }
                        }
                        else
                        {
                            employeeEnterArchive.Date = DateTime.Now;
                            employeeEnterArchive.isSuccessfull = false;
                            employeeEnterArchive.Login = LoginTB.Text;
                            ShowMessage.ErrMessage("Неправильный логин или пароль!");
                            NavigationService.Navigate(new CaptchaPage());
                        }
                        Context._con.EmployeeEnterArchive.Add(employeeEnterArchive);
                        Context._con.SaveChanges();
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
                        if (WindowWithFrame.employee != null)
                        {
                            employeeEnterArchive.Date = DateTime.Now;
                            employeeEnterArchive.EmployeeId = WindowWithFrame.employee.Id;
                            employeeEnterArchive.isSuccessfull = true;
                            employeeEnterArchive.Login = LoginTB.Text;
                            if (WindowWithFrame.employee.RoleId == 1)
                            {
                                NavigationService.Navigate(new LaborantPage());
                            }
                            else if (WindowWithFrame.employee.RoleId == 2)
                            {
                                NavigationService.Navigate(new LaborantIsslPage());
                            }
                            else if (WindowWithFrame.employee.RoleId == 3)
                            {
                                NavigationService.Navigate(new AdminPage());
                            }
                            else if (WindowWithFrame.employee.RoleId == 4)
                            {
                                NavigationService.Navigate(new AccounterPage());
                            }
                        }
                        else
                        {
                            employeeEnterArchive.Date = DateTime.Now;
                            employeeEnterArchive.isSuccessfull = false;
                            employeeEnterArchive.Login = LoginTB.Text;
                            ShowMessage.ErrMessage("Неправильный логин или пароль!");
                            NavigationService.Navigate(new CaptchaPage());
                        }
                        Context._con.EmployeeEnterArchive.Add(employeeEnterArchive);
                        Context._con.SaveChanges();
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

        private void PasswordPB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                this.SignIn(sender, e);
            }
        }

        private void LoginTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordPB.Focus();
            }
        }
    }
}
