using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Patients
{
    public class MunicipalityVM : LookupTableBaseViewModel
    {
        public string Capital { get; set; }
        public int StateId { get; set; }
    }
}
