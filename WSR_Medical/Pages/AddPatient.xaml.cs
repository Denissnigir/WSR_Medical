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
        List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>();
        List<InsuranceType> insuranceTypes = new List<InsuranceType>();
        Patient patientData;
        Patient patientSender;

        public AddPatient(Patient patient = null)
        {
            InitializeComponent();
            patientSender = patient;
            if (patient == null)
            {
                patientData = new Patient();
            }
            else
            {
                patientData = patient;
            }
            MainGrid.DataContext = patientData;
            insuranceCompanies = Context._con.InsuranceCompany.ToList();
            insuranceTypes = Context._con.InsuranceType.ToList();
            insuranceCompanies.Insert(0, new InsuranceCompany { Name = "Выберите компанию" });
            insuranceTypes.Insert(0, new InsuranceType { Name = "Выберите тип" });
            InsuranceTypeCB.ItemsSource = insuranceTypes;
            InsuranceNameCB.ItemsSource = insuranceCompanies;
            if (patient != null)
            {
                InsuranceTypeCB.SelectedIndex = patient.InsuranceTypeId;
                InsuranceNameCB.SelectedIndex = patient.InsuranceCompanyId;
            }
            else
            {
                InsuranceTypeCB.SelectedIndex = 0;
                InsuranceNameCB.SelectedIndex = 0;
            }

        }

        private void AddPatientClick(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = (DateTime)BirthDateDP.SelectedDate;
            long ticks = dateTime.Ticks;
            patientData.BirthDate = ticks;
            if(patientSender == null)
            {
                Context._con.Patient.Add(patientData);
            }
            Context._con.SaveChanges();
            if(patientSender == null)
            {
                ShowMessage.InfMessage("Пациент зарегестрирован!");
            }
            else 
            {
                ShowMessage.InfMessage("Пациент отредактирован!");
            }
            NavigationService.Navigate(new AddBiomaterial());
        }
    }
}
