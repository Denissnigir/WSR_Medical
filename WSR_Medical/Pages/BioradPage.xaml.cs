using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using WSR_Medical.Utils;
using System.Windows.Threading;
using WSR_Medical.Windows;
using System.Threading;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для BioradPage.xaml
    /// </summary>
    public partial class BioradPage : Page
    {
        string api = "http://localhost:5000/api/analyzer/Biorad";
        HttpClient httpClient;

        DispatcherTimer dispatcherTimer;
        DispatcherTimer dispatcherTimerRotate;
        TimeSpan timerCounter;

        int rotate = 0;
        List<Image> images = new List<Image>();

        public BioradPage()
        {
            InitializeComponent();
            ServiceList.ItemsSource = Context._con.OrderService.Where(p => p.StatusId == 1 && p.Service.AnalyzerService.FirstOrDefault().AnalyzerId == 2).ToList();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += timerTick;
            dispatcherTimerRotate = new DispatcherTimer();
            dispatcherTimerRotate.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimerRotate.Tick += DispatcherTimerRotate_Tick;
            dispatcherTimerRotate.Start();

        }

        private void DispatcherTimerRotate_Tick(object sender, EventArgs e)
        {
            RotateTransform rotateTransform1 = new RotateTransform(rotate, 0, 0);

            foreach (var image in images)
            {
                image.RenderTransform = rotateTransform1;
            }
            rotate = rotate < 360 ? rotate + 20 : 0;

        }

        public static OrderService serviceData;
        private async void SendService(object sender, RoutedEventArgs e)
        {
            if (ServiceList.SelectedItem is OrderService service)
            {

                serviceData = service;
                var content = new responceAnalyzer { patient = service.Order.Biomaterial.PateintId.ToString(), services = new responceService[] { new responceService { serviceCode = service.ServiceId }, new responceService { serviceCode = service.ServiceId }, new responceService { serviceCode = service.ServiceId }, new responceService { serviceCode = service.ServiceId }, new responceService { serviceCode = service.ServiceId } } };
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.PostAsync(api, httpContent);

                if ("\"Analyzer is busy\"" == await response.Content.ReadAsStringAsync())
                {
                    ShowMessage.InfMessage("Анализатор занят");
                    return;
                }
                timerCounter = new TimeSpan(0, 0, 0);


                dispatcherTimer.Start();

            }
        }

        private async void timerTick(object sender, EventArgs e)
        {
            timerCounter += TimeSpan.FromSeconds(1);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(api);
            //MessageBox.Show(await responseMessage.Content.ReadAsStringAsync()); // короче вот эта дрянь не даёт идти таймеру дальше и после вывода месседжбокса таймертик идёт по новой) 
            var status = await responseMessage.Content.ReadAsStringAsync();
            if (status.Contains("progress"))
            {
                serviceData.StatusId = 2;
                serviceData.Procent = JsonConvert.DeserializeObject<ProcentClass>(status).progress;
                ServiceList.ItemsSource = (ServiceList.ItemsSource as List<OrderService>).ToList();
            }

            if (status.Contains("patient"))
            {
                Context._con.SaveChanges();
                MessageBox.Show(await responseMessage.Content.ReadAsStringAsync());
                ServiceList.ItemsSource = Context._con.OrderService.Where(p => p.StatusId == 1 && p.Service.AnalyzerService.FirstOrDefault().AnalyzerId == 2).ToList();
                AnalyzerLog analyzerLog = new AnalyzerLog();
                analyzerLog.AnalyzerId = 2;
                analyzerLog.EmployeeId = WindowWithFrame.employee.Id;
                analyzerLog.OrderId = serviceData.OrderId;
                analyzerLog.ServiceDate = DateTime.Now;
                var result = JsonConvert.DeserializeObject<responceAnalyzer>(status).services.FirstOrDefault().result;

                var averageResult = Context._con.OrderService.Where(i => i.ServiceId == serviceData.ServiceId).Average(p => p.Result);
                
                double res = 0;
                if (result == "POS" || result == "neg" || result == "+/-")
                {
                    res = result == "POS" ? 1 : result == "+/-" ? 0.5 : 0;
                }
                else
                {
                    res = Convert.ToDouble(result.Replace(".", ","));
                }

                if (res <= averageResult * 5 || res >= averageResult / 5)
                {
                    dispatcherTimer.Stop();

                    showM();

                }

                serviceData.Result = res;
                analyzerLog.Result = res;
                analyzerLog.Finished = DateTime.Now;
                dispatcherTimer.Stop();
                if (MessageBox.Show($"{result}: Согласен?", "Точно согласен?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    analyzerLog.Accepted = true;
                    analyzerLog.StatusId = 1;
                }
                else
                {
                    analyzerLog.Accepted = false;
                    analyzerLog.StatusId = 2;
                }
                Context._con.AnalyzerLog.Add(analyzerLog);
                Context._con.SaveChanges();

            }
            if (timerCounter >= new TimeSpan(0, 0, 30))
            {

                dispatcherTimer.Stop();
            }
        }

        private void showM()
        {
            MessageBox.Show("хуита хует");
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            images.Add(sender as Image);
        }
    }

    public class ProcentClass
    {
        public int progress { get; set; }
    }
}
