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
using WSR_Medical.Utils;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page
    {
        Patient patient;
        List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>();
        List<InsuranceType> insuranceTypes = new List<InsuranceType>();

        public AddPatient()
        {
            InitializeComponent();
            patient = new Patient();
            MainGrid.DataContext = patient;
            insuranceCompanies = Context._con.InsuranceCompany.ToList();
            insuranceTypes = Context._con.InsuranceType.ToList();
            insuranceCompanies.Insert(0, new InsuranceCompany { Name = "Выберите компанию" });
            insuranceTypes.Insert(0, new InsuranceType { Name = "Выберите тип" });
            InsuranceTypeCB.ItemsSource = insuranceTypes;
            InsuranceNameCB.ItemsSource = insuranceCompanies;
            InsuranceTypeCB.SelectedIndex = 0;
            InsuranceNameCB.SelectedIndex = 0;

            ShowMessage.InfMessage("Пациент зарегестрирован!");
            NavigationService.Navigate(new AddBiomaterial());
        }

        private void AddPatientClick(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = (DateTime)BirthDateDP.SelectedDate;
            long ticks = dateTime.Ticks;
            patient.BirthDate = ticks;
            Context._con.Patient.Add(patient);
            Context._con.SaveChanges();
        }
    }
}
