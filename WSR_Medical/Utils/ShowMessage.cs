using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WSR_Medical.Utils
{
    public static class ShowMessage
    {
        public static void ErrMessage(string message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void InfMessage(string message)
        {
            
            MessageBox.Show(message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
