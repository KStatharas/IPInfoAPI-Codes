using IP2C_IPInfoProvider.Models;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;

namespace IPInfoAPI_Codes.Services
{
    public interface IIPInfoService
    {
        public Task<CountryDTO> GetCountry(string ip);
        public Task<List<IPInfo>> UpdateIPInfo(int uCount, int iteration);
        public Task<int> CountStoredIps();

        public Task<IEnumerable<CountryReportDTO>> GetIpReport(List<String>? twoLetterCodes);
    }
}
