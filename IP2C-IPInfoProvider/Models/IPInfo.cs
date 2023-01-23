using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2C_IPInfoProvider.Models
{
    public class IPInfo : IIPInfo
    {
        public string IP { get; set; }
        public string Country_Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public DateTime GenerationDate { get; set; }
    }
}
