using IP2C_IPInfoProvider.Models;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;
using IPInfoAPI_Codes.Repositories;

namespace IPInfoAPI_Codes.Services
{
    public interface IIPInfoService
    {
        /// <summary>
        /// GetCountry accesses the database to obtain information and return a CountryDTO object based on the IP provided by the user.
        /// If no such IP is stored in the database, the IP2C-IPInfoProvider service is called to obtain all relevant IP information
        /// which is then stored in the database.
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns>A <see cref="CountryDTO">CountryDTO</see> object</returns>
        public Task<CountryDTO> GetCountry(string ip);

        /// <summary>
        /// UpdateIPInfo method receives the batch size (<param name="uCount">uCount</param>) as a parameter
        /// which determines the number of IPs that will be retrieved with each iteration.
        /// <param name="iteratedIps">IteratedIps</param> parameter passes the number of the up-to-date Ips.
        /// For every IP, the IP2C API is called. If an IP has outdated information it is added to a list and then is updated.
        /// This list will be later used by Cache which will check its storage for any outdated information.
        /// </summary>
        /// <param name="iteratedIps">iteratIps</param>
        /// <param name="uCount">uCount</param>
        /// <returns> A <see cref="IPInfo">IpInfo list</see> with the outdated IPs information.</returns>
        public Task<List<IPInfo>> UpdateIPInfo(int uCount, int iteratedIps);

        /// <summary>
        /// CountStoredIps is a method used to count the number of the IPs stored in the database.
        /// </summary>
        /// <returns> An <see cref="int">integer</see> number</returns>
        public Task<int> CountStoredIps();

        /// <summary>
        /// GetIpReport creates a raw sql query that is used by Dapper to return a collection of CountryReportDTOs which
        /// consist of a country's name along with the number of Ips that belong to that country as well as the last time one of them was updated.
        /// As a parameter a list of country two-letter codes can be passed. In that case the returned collection includes only countries represented by these two-letter codes.
        /// If the list is null, all countries stored in the database are included.
        /// </summary>
        /// <param name="twoLetterCodes"></param>
        /// <returns> A <see cref="CountryReportDTO">CountryReportDTO</see> collection</returns>
        public Task<IEnumerable<CountryReportDTO>> GetIpReport(List<String>? twoLetterCodes);
    }
}
