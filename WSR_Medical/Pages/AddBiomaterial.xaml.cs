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
using WSR_Medical.Windows;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBiomaterial.xaml
    /// </summary>
    public partial class AddBiomaterial : Page
    {
        List<string> barcode = new List<string>();
        Patient patient;
        Biomaterial biomaterial = new Biomaterial();
        Order order = new Order();
        OrderService orderService = new OrderService();
        List<Service> services = new List<Service>();
        
        public AddBiomaterial()
        {
            InitializeComponent();
            var lastBarcode = Context._con.Biomaterial.OrderByDescending(p => p.Id).FirstOrDefault();
            string curBarcode = Convert.ToString(Convert.ToInt64(lastBarcode.Barcode) + 1);
            barcode.Add(curBarcode);
            BarcodeName.ItemsSource = barcode;
            patient = new Patient();
            PatientGrid.DataContext = patient;
            ServiceList.ItemsSource = services;
            ServiceCB.ItemsSource = Context._con.Service.ToList();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new AddPatient());
        }

        private void CheckBiomaterialNumber(object sender, RoutedEventArgs e)
        {
            var biomat = Context._con.Biomaterial.Where(p => p.Barcode == BarcodeName.Text).FirstOrDefault();
            if(biomat == null)
            {
                PatientGrid.Visibility = Visibility.Visible;
                BarcodeName.IsEditable = false;
            }
            else
            {
                ShowMessage.InfMessage("Введите другой штрихкод!");
            }
        }

        private void BarcodeName_TextInput(object sender, TextCompositionEventArgs e)
        {
            BarcodeName.IsDropDownOpen = true;
            
        }

        string endpoint = @"https://wsrussia.ru/?data=";
        int curPrice = 0;
        private void AddService(object sender, RoutedEventArgs e)
        {
            if (ServiceCB.SelectedItem is Service service)
            {
                services.Add(new Service { Name = service.Name, Id = service.Id });
                curPrice += Convert.ToInt32(service.Price);
                PriceTB.Text = $"Price: {curPrice}";
            }
            ServiceList.ItemsSource = services.ToList();

        }

        private void AddBiomaterialClick(object sender, RoutedEventArgs e)
        {
            var patient = Context._con.Patient.Where(p => p.FirstName == FirstNameCB.Text && p.SecondName == SecondNameCB.Text && p.MiddleName == MiddleNameCB.Text).FirstOrDefault();
            if(patient != null)
            {
                if(ServiceList.Items.Count != 0)
                {
                    biomaterial.Barcode = BarcodeName.Text;
                    biomaterial.Date = DateTime.Now.Ticks;
                    biomaterial.PateintId = patient.Id;
                    Context._con.Biomaterial.Add(biomaterial);
                    Context._con.SaveChanges();

                    order.DateStart = DateTime.Now;
                    order.StatusId = 1;
                    order.BiomaterialId = biomaterial.Id;
                    order.EmployeeId = WindowWithFrame.employee.Id;
                    Context._con.Order.Add(order);
                    Context._con.SaveChanges();

                    foreach(var item in services)
                    {
                        orderService.OrderId = order.Id;
                        orderService.ServiceId = item.Id;
                        orderService.StatusId = 1;
                        Context._con.OrderService.Add(orderService);
                        Context._con.SaveChanges();
                    }

                    string result = endpoint + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"дата_заказа={DateTime.Now}&номер_заказа={order.Id}"));
                    File.WriteAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\1.txt", result);

                    NavigationService.Navigate(new PrintPage(biomaterial, curPrice));
                }
                else
                {
                    ShowMessage.ErrMessage("Выберите услугу!");
                }
            }
            else
            {
                ShowMessage.ErrMessage("Пациент не найден!");
            }
        }

        private void SecondNameCB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var patients = Context._con.Patient.ToList().Where(p => Levenshtein.GetLev(SecondNameCB.Text, p.SecondName));
            SecondNameCB.ItemsSource = patients;
            FirstNameCB.ItemsSource = patients;
            MiddleNameCB.ItemsSource = patients;
            SecondNameCB.IsDropDownOpen = true;
        }

        private void SecondNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SecondNameCB.SelectedItem is Patient patient)
            {
                FirstNameCB.SelectedItem = patient;
                MiddleNameCB.SelectedItem = patient;
            }
        }

        private void FirstNameCB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var patients = Context._con.Patient.ToList().Where(p => Levenshtein.GetLev(FirstNameCB.Text, p.FirstName));
            SecondNameCB.ItemsSource = patients;
            FirstNameCB.ItemsSource = patients;
            MiddleNameCB.ItemsSource = patients;
            FirstNameCB.IsDropDownOpen = true;
        }

        private void FirstNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstNameCB.SelectedItem is Patient patient)
            {
                SecondNameCB.SelectedItem = patient;
                MiddleNameCB.SelectedItem = patient;
            }
        }
    }
}
