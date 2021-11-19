using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSR_Medical.Model;

namespace WSR_Medical.Model
{
    public partial class Patient
    {
        public string GetName
        {
            get
            {
                string fullName = $"{FirstName} {SecondName} {MiddleName}";
                return fullName;
            }
        }

        public string GetServices
        {
            get
            {

                return Biomaterial.FirstOrDefault().GetServices;
            }
        }
    }
}
