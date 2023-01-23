using IP2C_IPInfoProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2C_IPInfoProvider.Services
{
    public interface IIP2C
    {
        public IPInfo getIPCountryDetails(string ip);
    }
}
