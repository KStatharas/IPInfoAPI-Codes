using Azure.Core;
using IPInfoAPI_Codes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics.Metrics;
namespace IPInfoAPI_Codes.Repositories
{
    public interface IIPInfoController
    {
        /// <summary>
        /// In the GetIp endpoint a user passes an IP to the url and receives the name of the country this IP belongs to
        /// as well as the two-letter and three-letter code of that country.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Task<IActionResult> GetIP(string ip);

        ///  <summary>
        ///The GetReport endpoint allows a user to make an empty request to retrieve information about all the countries stored in the database
        ///or pass a list of two-letter codes to get information about the countries they represent.
        ///The retrieved information includes a collection of country names along with the total number of IPs belonging to each country 
        ///and the last time one of those IPs was updated.
        /// </summary>
        /// <param name="twoLetterCodes"></param>
        public Task<IActionResult> GetReport(List<String>? twoLetterCodes);
    }
}
