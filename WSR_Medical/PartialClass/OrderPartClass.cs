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
                    Console.WriteLine(x.Service.Name);
                    result += x.Service.Name;

                }
                Console.WriteLine(OrderService.Count);
                return result;
            }
        }
    }
}
