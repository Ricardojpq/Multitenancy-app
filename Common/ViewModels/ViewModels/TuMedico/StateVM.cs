using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Patients
{
    public class StateVM:LookupTableBaseViewModel
    {
        public string Capital { get; set; }
        public int CountryId { get; set; }
    }
}
