using System;
using System.Collections.Generic;
using System.IO;
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
using WSR_Medical.Utils;

namespace WSR_Medical
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    //    private void ImportUser()
    //    {
    //        var fileData = File.ReadAllLines(@"C:\Users\0dmin\Desktop\WorldSkills\8\user.txt");

    //        foreach (var line in fileData)
    //        {
    //            var data = line.Split('\t');
    //            var date = new DateTime();
    //            try
    //            {
    //                date = Convert.ToDateTime(data[7]);
    //            }
    //            catch
    //            {
    //                var datestring = data[7].Split('.');
    //                date = Convert.ToDateTime($"{datestring[1]}.{datestring[0]}.{datestring[2]}");
    //            }
    //            Console.WriteLine(data[7]);

    //            var tempEmployee = new Employee
    //            {
    //                FirstName = data[1],
    //                SecondName = data[2],
    //                MiddleName = data[3],
    //                Login = data[4],
    //                Password = data[5],
    //                Ip = data[6],
    //                Lastenter = date,
    //                RoleId = data[9].ToInt()
    //            };


    //            Context._con.Employee.Add(tempEmployee);
    //            Context._con.SaveChanges();
    //            foreach (var serviceType in data[8].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
    //            {
    //                var currentService = Context._con.Service.ToList().FirstOrDefault(p => p.Id == serviceType.ToInt());
    //                if (currentService != null)
    //                {
    //                    Context._con.EmployeeService.Add(new EmployeeService { EmployeeId = tempEmployee.Id, ServiceId = currentService.Id });
    //                }
    //            }
    //            Context._con.SaveChanges();
    //        }
    //    }
    //}
}
