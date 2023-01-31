using Castle.Components.DictionaryAdapter.Xml;
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


        //GetCountry is one of the methods implemented by the cache.
        //If the cache has already created an entry that has as a key the IP passed as a parameter in the GetCountry method,
        //the cache returns the result without accessing the database again.
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

        //If an IP is stored in the cache and its information get changed during
        //a database update, it will be removed from the cache.
        public void SyncCache(List<IPInfo> changedIps)
        {
            foreach (IPInfo ipItem in changedIps)
            {
                if (_cache.Get(ipItem.IP)!=null) _cache.Remove(ipItem.IP);
            }
        }

        public async Task<IEnumerable<CountryReportDTO>> GetIpReport(List<String>? twoLetterCodes)
        {
            return await _service.GetIpReport(twoLetterCodes);
        }

        public Task<List<IPInfo>> UpdateIPInfo(int uCount, int iteration)
        {
            return _service.UpdateIPInfo(uCount, iteration);
        }

        public async Task<int> CountStoredIps()
        {
            return await _service.CountStoredIps();
        }


    }
}
