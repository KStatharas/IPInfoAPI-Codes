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
        /// <summary>
        /// This is a method that makes a Webclient connection with the external API "IP2C"
        /// by passing it an <param name="ip">IP</param> in order to retrieve its country's full name, two-letter and three letter code
        /// as well as a status code (a number) that determines if the operation succedeed.
        /// <para>
        /// <see href="http://about.ip2c.org/">IP2C</see>
        /// <param name="ip">IP</param>
        /// </para>
        /// </summary>
        /// <returns>An <see cref="IPInfo">IPInfo</see> object</returns>
        public IPInfo getIPCountryDetails(string ip);
    }
}
