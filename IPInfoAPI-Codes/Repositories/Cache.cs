using IP2C_IPInfoProvider.Models;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;
using IPInfoAPI_Codes.Services;
using Microsoft.Extensions.Caching.Memory;

namespace IPInfoAPI_Codes.Repositories
{
    public class Cache : IIPInfoService,ICache
    {
        private readonly IPInfoServiceImpl _service;
        private readonly IMemoryCache _cache;

        public Cache(IPInfoServiceImpl service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        

        public async Task<CountryDTO> GetCountry(string ip)
        {
            string key = $"{ip}";

            return await _cache.GetOrCreate(
                key,
                async entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                    return await _service.GetCountry(ip);
                })!;

        }

        public void SyncCache(List<IPInfo> changedIps)
        {
            foreach (IPInfo ipItem in changedIps)
            {
                if (_cache.Get(ipItem.IP)!=null) _cache.Remove(ipItem.IP);
            }
        }

        public Task<List<IPInfo>> UpdateIPInfo(int uCount, int iteration)
        {
            return _service.UpdateIPInfo(uCount, iteration);
        }

        public async Task<int> CountStoredIps()
        {
            return await _service.CountStoredIps();
        }

        public async Task<IEnumerable<CountryReportDTO>> GetIpReport(List<String>? twoLetterCodes)
        {
           return await _service.GetIpReport(twoLetterCodes);
        }
    }
}
