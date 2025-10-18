using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class CurrencyTest
    {
      //public string code { get; set; }
      //public string msg { get; set; }
      public List<CurrencyCode> supported_codes { get; set; }
    }

    public class  CurrencyCode
    {
        public string code { get; set; }
        public string name { get; set; }
    }
}
