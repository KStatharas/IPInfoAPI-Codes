using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2C_IPInfoProvider.Exceptions
{
    public class IPNotFoundException : Exception
    {
        public IPNotFoundException(string ip) : base($"This IP does not exist: {ip}")
        {
        }
    }
}
