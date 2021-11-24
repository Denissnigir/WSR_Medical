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
    /// Логика взаимодействия для ServiceReport.xaml
    /// </summary>
    public partial class ServiceReport : Page
    {
        List<OrderService> orderServices = new List<OrderService>();

        List<Analytics> analytics = new List<Analytics>();
        public ServiceReport()
        {
            InitializeComponent();
            int patientNumber = Context._con.OrderService.Select(i => i.Order.Biomaterial.PateintId).Distinct().Count();
            orderServices = Context._con.OrderService.ToList();
            Filter();
        }

        public void Filter()
        {
            analytics.Clear();
            if(FirstDateDP.SelectedDate is DateTime && SecondDateDP.SelectedDate is DateTime)
            {
                orderServices = Context._con.OrderService.Where(p => p.Order.DateStart >= FirstDateDP.SelectedDate && p.Order.DateStart <= SecondDateDP.SelectedDate).ToList();

                DateTime? firstDate = FirstDateDP.SelectedDate;
                DateTime? secondDate = SecondDateDP.SelectedDate;

                for (DateTime first = firstDate.Value; first <= secondDate; first = first.AddDays(1))
                {
                    orderServices = Context._con.OrderService.ToList().Where(p => p.Order.DateStart >= first && p.Order.DateStart < first.AddDays(1)).ToList();
                    analytics.Add(new Analytics
                    {
                        serviceCount = orderServices.Count,
                        serviceName = string.Join(", ", orderServices.Select(i => i.Service.Name)),
                        patientCount = orderServices.Select(i => i.Order.Biomaterial.PateintId)
                                                    .Distinct()
                                                    .Count(),
                        serviceNameCount = string.Join(", ",
                                orderServices.Select(p => $"{first} {p.Service.Name} = {Context._con.OrderService.Where(i => i.ServiceId == p.ServiceId).Select(i => i.Order.Biomaterial.PateintId).Distinct().Count()}")),
                        averageResult = orderServices.Select(p => p.Result ?? 0).Sum() / orderServices.Select(p => p.Result.HasValue).Count()
                    });
                }


            }
            ReportGrid.ItemsSource = analytics.ToList();
        }

        private void FirstDateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }

    class Analytics
    {
        public DateTime date { get; set; }
        public int serviceCount { get; set; }
        public string serviceName { get; set; }
        public int patientCount { get; set; }
        public string serviceNameCount { get; set; }
        public double averageResult { get; set; }
    }
}
