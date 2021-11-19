using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
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
    /// Логика взаимодействия для LedetectPage.xaml
    /// </summary>
    public partial class LedetectPage : Page
    {
        public static readonly HttpClient httpClient = new HttpClient();

        string api = "http://localhost:5000/api/analyzer/Ledetect";

        public LedetectPage()
        {
            InitializeComponent();
            ServiceList.ItemsSource = Context._con.OrderService.Where(p => p.StatusId == 1 && p.Service.AnalyzerService.FirstOrDefault().AnalyzerId == 1).ToList();
        }

        private async void SendService(object sender, RoutedEventArgs e)
        {
            if (ServiceList.SelectedItem is OrderService service)
            {
                service.StatusId = 2;
                Context._con.SaveChanges();

                var analyzerContent = new responceAnalyzer { patient = service.Order.Biomaterial.PateintId.ToString(), services = new responceService[] { new responceService { serviceCode = service.ServiceId } } };
                HttpContent content = new StringContent(Serializer.JsonSerialization<responceAnalyzer>(analyzerContent), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(api, content);

                if("\"Analyzer is busy\"" == await response.Content.ReadAsStringAsync())
                {
                    MessageBox.Show("Анализатор занят");
                    return;
                }

                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
        }

    }

    public class responceAnalyzer
    {
        public string patient { get; set; }
        public responceService[] services { get; set; }
    }


    public class responceService
    {
        public int serviceCode { get; set; }
    }
}
