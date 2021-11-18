using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_Medical.Utils
{
    public static class Converter
    {
        public static int ToInt(this object obj) => Convert.ToInt32(obj);

    }
}
