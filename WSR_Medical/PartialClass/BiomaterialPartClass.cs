using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_Medical.Model
{
    public partial class Biomaterial
    {
        public int GetId { get { return Order.FirstOrDefault().Id; } }

        public string GetServices
        {
            get
            {
                
                return Order.FirstOrDefault().GetServices;
            }
        }
    }
}
