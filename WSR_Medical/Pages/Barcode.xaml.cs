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

namespace WSR_Medical.Pages
{
    /// <summary>
    /// Логика взаимодействия для Barcode.xaml
    /// </summary>
    public partial class Barcode : Page
    {
        public Barcode()
        {
            InitializeComponent();
            int[] number = new int[] { 1, 8, 9, 3, 4, 2, 7, 1, 4 };
            foreach(var n in number)
            {
                var distance = Convert.ToDouble(Convert.ToInt32(n)) * 2.15;
                Line line = new Line();
                line.X1 = 0;
                line.Y1 = 200;
                line.Margin = new Thickness(5);
                line.StrokeThickness = distance;
                line.Stroke = Brushes.Black;
                BarcodeStack.Children.Add(line);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog())
            {
                printDialog.PrintVisual((Visual)BarcodeStack, "desc");
            }
        }
    }
}
