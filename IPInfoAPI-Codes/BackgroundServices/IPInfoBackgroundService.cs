using IP2C_IPInfoProvider.Models;
using IPInfoAPI_Codes.Repositories;
using IPInfoAPI_Codes.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.IsolatedStorage;

namespace IPInfoAPI_Codes.BackgroundServices
{
    public class IPInfoBackgroundService : BackgroundService
    {
        private readonly ILogger<IPInfoBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly int batchSize = 100;

        public IPInfoBackgroundService(ILogger<IPInfoBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("IPInfo API background service has been started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                //The "await" keyword in the line below is important in order to let the API start running.
                await Task.Delay(360000);

                await SyncDatabase();

            }
        }


        private async Task SyncDatabase()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IIPInfoService _service = scope.ServiceProvider.GetRequiredService<IPInfoServiceImpl>();

                int numOfStoredIps = await _service.CountStoredIps();
                int iterated = 0;

                

                while (iterated < numOfStoredIps)
                {
                    List<IPInfo> changedIpInfo = await _service.UpdateIPInfo(batchSize, iterated);
                    UpdateCache(changedIpInfo);

                    iterated+=batchSize;
                }
            }
        }

        private void UpdateCache(List<IPInfo> changedIps)
        {
            using (IServiceScope cacheScope = _serviceProvider.CreateScope())
            {
                ICache _cache = cacheScope.ServiceProvider.GetRequiredService<Cache>();
                _cache.SyncCache(changedIps);
            }
        }
    }
}
