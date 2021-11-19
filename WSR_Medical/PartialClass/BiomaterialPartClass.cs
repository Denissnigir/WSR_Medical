using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_Medical.Model
{
    public partial class Biomaterial
    {
        public int GetId { get { return Order?.FirstOrDefault()?.Id ?? 0; } }

        public string GetServices
        {
            get
            {
                return Order?.FirstOrDefault()?.GetServices ?? "Нет заказа";
            }
        }

        public string GetPrice
        {
            get
            {
                Console.WriteLine(Order?.FirstOrDefault()?.GetPrice ?? "Нет цены");
                return Order?.FirstOrDefault()?.GetPrice ?? "Нет цены";
            }
        }

        public decimal GetTotalPrice
        {
            get
            {
                
                return Order?.FirstOrDefault()?.GetTotalPrice ?? 0;
            }
        }
    }
}
