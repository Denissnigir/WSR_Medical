using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WSR_Medical.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            ChartReport.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Reports")
            {
                IsValueShownAsLabel = true
            };
            ChartReport.Series.Add(currentSeries);
            ChartTypeCB.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }
        double result = 0;

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ChartTypeCB.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartReport.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();
                double c = 0;
                //foreach (var a in Context._con.OrderService.Where(p => p.Result.HasValue))
                //{
                //    var b = Context._con.OrderService.Where(p => p.ServiceId == a.ServiceId).FirstOrDefault();
                //    c = Math.Pow(b.Result.Value - Context._con.OrderService.Where(p => p.ServiceId == a.ServiceId).Count(), 2);
                //}
                //result = Math.Sqrt(c / Context._con.OrderService.Count());
                var sum = 0.0;
                var service = Context._con.OrderService.Where(p => p.Result.HasValue);
                foreach (var serv in service)
                {
                    var X = serv.Result - Context._con.OrderService.Select(i => i.Result).Average();
                    sum += Math.Pow(X.Value, 2);
                }

                var result = Math.Sqrt(sum / service.Count());
                //for (int i = 1; i <= Context._con.OrderService.Count(); i++)
                //{
                //    var a = Context._con.OrderService.Where(p => p.Id == i).FirstOrDefault();
                //    double? b = Context._con.OrderService.Where(p => p.ServiceId == a.ServiceId).Sum(p => p.Result);
                //    b = Math.Pow(b.Value / Context._con.OrderService.Where(p => p.ServiceId == a.ServiceId).Count(), 2);
                //    result += Math.Sqrt(b.Value / Context._con.OrderService.Count()); 
                //}


                double averageResult = Context._con.AnalyzerLog.Sum(p => p.Result) / Context._con.AnalyzerLog.Count();
                double s1 = averageResult + (result * 1);
                double s2 = averageResult + (result * 2);
                double s3 = averageResult + (result * 3);
                double s1m = averageResult - (result * 1);
                double s2m = averageResult - (result * 2);
                double s3m = averageResult - (result * 3);
                double[] vs = new double[]
                {
                    s1,
                    s2,
                    s3,
                    s1m,
                    s2m,
                    s3m
                };


                var logList = Context._con.AnalyzerLog.ToList();
                foreach (var log in logList)
                {
                    currentSeries.Points.AddXY(log.ServiceDate.ToString(), log.Result);

                }

            }
        }

        private void PrintClick(object sender, RoutedEventArgs e)
        {
            ChartReport.Printing.Print(true);
        }

        private void PrintToXls(object sender, RoutedEventArgs e)
        {


            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            worksheet.Name = "Контроль качества";
            int startRow = 1;

            worksheet.Cells[1][startRow] = "Сервис";
            worksheet.Cells[2][startRow] = "Дата";
            worksheet.Cells[3][startRow] = "Результат";

            startRow++;

            var services = Context._con.OrderService.Where(p => p.Result.HasValue).ToList();
            MessageBox.Show(services.Count().ToString());
            foreach (var service in services)
            {
                worksheet.Cells[1][startRow] = service.Service.Name;
                worksheet.Cells[2][startRow] = service.Order.DateStart.ToString();
                worksheet.Cells[3][startRow] = service.Result;
                startRow++;
            }

            worksheet.Cells[1][startRow] = "Среднеквадратичное отклонение(ублюдки):";
            worksheet.Cells[2][startRow] = result.ToString();

            application.Visible = true;
        }
    }
}
