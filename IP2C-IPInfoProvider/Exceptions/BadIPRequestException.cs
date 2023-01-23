using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2C_IPInfoProvider.Exceptions
{
    internal class BadIPRequestException : Exception
    {
        public BadIPRequestException(string ip) : base($"This IP request is invalid: {ip}") {}
    }
}
