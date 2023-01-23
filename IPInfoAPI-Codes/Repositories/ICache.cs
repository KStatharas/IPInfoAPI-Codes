using IP2C_IPInfoProvider.Models;

namespace IPInfoAPI_Codes.Repositories
{
    public interface ICache
    {
        public void SyncCache(List<IPInfo> changedIps);
    }
}
