using IP2C_IPInfoProvider.Exceptions;
using IP2C_IPInfoProvider.Models;
using System;
using System.Net;

namespace IP2C_IPInfoProvider.Services
{
    
    public class IP2CImpl : IIP2C
    {
        
        public IPInfo getIPCountryDetails(string ip)
        {
            using (WebClient url = new WebClient())
            {
                //Using WebClient we manage to retrieve a string given by IP2C API.
                string ipInfo = url.DownloadString("http://ip2c.org/?ip=" + ip);

                switch (ipInfo[0])
                {
                    case '0':
                        throw new BadIPRequestException(ip);
                        break;

                    case '1':
                        string[] reply = ipInfo.Split(';');
                        IPInfo ipCountryInfo = new IPInfo()
                        {
                            IP = ip,
                            TwoLetterCode = reply[1],
                            ThreeLetterCode = reply[2],
                            Country_Name = reply[3],
                            GenerationDate = DateTime.Now
                        };
                        return ipCountryInfo;

                    case '2':
                        throw new IPNotFoundException(ip);
                        break;

                    default:
                        return new();
                }
            }
        }
    }
}
