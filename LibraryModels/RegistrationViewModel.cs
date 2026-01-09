using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class RegistrationViewModel
    {
        public Reader Reader{get;set;}
        public List<City> Cities { get; set; }
    }
}
