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
    /// Логика взаимодействия для SignInHistory.xaml
    /// </summary>
    public partial class SignInHistory : Page
    {
        List<Employee> employees = new List<Employee>();
        List<EmployeeEnterArchive> loginList = new List<EmployeeEnterArchive>();
        List<string> dateList = new List<string>()
        {
            "По дате(по возрастанию)",
            "По дате(по убыванию)"
        };
        public SignInHistory()
        {
            InitializeComponent();
            loginList = Context._con.EmployeeEnterArchive.ToList();
            employees = Context._con.Employee.ToList();
            LoginCB.ItemsSource = employees;
            DateCB.ItemsSource = dateList;
            DateCB.SelectedIndex = 0;
            Filter();
        }

        public void Filter()
        {
            if(LoginCB.SelectedIndex > 0)
            {
                loginList = loginList.Where(p => p.Login == (LoginCB.SelectedItem as Employee).Login).ToList();
            }

            HistoryList.ItemsSource = loginList;

            switch (DateCB.SelectedIndex)
            {
                case 0:
                    HistoryList.ItemsSource = loginList.OrderBy(p => p.Date);
                    break;
                case 1:
                    HistoryList.ItemsSource = loginList.OrderByDescending(p => p.Date);
                    break;
                default:
                    break;
            }
        }

        private void LoginCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
