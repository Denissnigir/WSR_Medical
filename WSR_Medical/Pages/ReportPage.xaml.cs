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

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ChartTypeCB.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartReport.Series.FirstOrDefault();
                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();
                var logList = Context._con.AnalyzerLog.ToList();
                foreach (var log in logList)
                {
                    currentSeries.Points.AddXY(log.ServiceDate.ToString(), log.Result);
                }
            }
        }
    }
}
