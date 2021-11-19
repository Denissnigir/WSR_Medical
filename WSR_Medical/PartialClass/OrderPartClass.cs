using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_Medical.Model
{
    public partial class Order
    {
        public string GetServices
        {
            get
            {
                string result = string.Empty;
                foreach(var x in Context._con.OrderService.Where(p => p.OrderId == Id).ToList())
                {
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        result += ", ";
                    }
                    result += x.Service.Name;

                }
                return result;
            }
        }

        public string GetPrice
        {
            get
            {
                string result = string.Empty;
                foreach (var x in Context._con.OrderService.Where(p => p.OrderId == Id).ToList())
                {
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        result += ", ";
                    }
                    result += x.Service.Price;

                }
                return result;
            }
        }

        public decimal GetTotalPrice
        {
            get
            {
                return Context._con.OrderService.Where(p => p.OrderId == Id).Sum(i => i.Service.Price);
            }
        }
    }
}
