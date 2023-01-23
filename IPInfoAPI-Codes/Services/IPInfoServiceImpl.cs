using Dapper;
using IP2C_IPInfoProvider.Exceptions;
using IP2C_IPInfoProvider.Models;
using IP2C_IPInfoProvider.Services;
using IPInfoAPI_Codes.Data;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;
using IPInfoAPI_Codes.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace IPInfoAPI_Codes.Services
{
    public class IPInfoServiceImpl : IIPInfoService
    {
        private readonly IPInfoAPIDbContext dbContext;
        private readonly IIP2C _IP2CService;

        public IPInfoServiceImpl(IPInfoAPIDbContext dbContext, IIP2C iP2CService)
        {
            this.dbContext = dbContext;
            _IP2CService = iP2CService;
        }

        public async Task<CountryDTO> GetCountry(string ip)
        {
            CountryDTO countryOutput = new CountryDTO();

            IPAddress address = await dbContext.IPAddresses.FirstOrDefaultAsync(address => address.IP == ip);

            if (address == null)
            {
                IPInfo newIp = _IP2CService.getIPCountryDetails(ip);

                Country countryItem = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == newIp.Country_Name);

                if (countryItem == null)
                {
                    countryItem = InfoSplit.ToCountry(newIp);

                    dbContext.Countries.Add(countryItem);
                    dbContext.SaveChanges();
                }

                IPAddress ipItem = InfoSplit.ToIPAddress(newIp, countryItem.Id);

                dbContext.IPAddresses.Add(ipItem);
                dbContext.SaveChanges();

                countryOutput = InfoSplit.ConvertToDTO(countryItem);
            }

            else
            {
                Country countryItem = await dbContext.Countries.FirstOrDefaultAsync(c => c.Id == address.CountryId);
                countryOutput = InfoSplit.ConvertToDTO(countryItem);
            }

            return countryOutput;
        }

        public async Task<List<IPInfo>> UpdateIPInfo(int uCount, int iteratedIps)
        {
            if (iteratedIps < 0 || uCount <= 0) throw new ArgumentException();


            List<IPAddress> ipItems = dbContext.IPAddresses.Skip(iteratedIps).Take(uCount).ToList();
            List<IPInfo> changedIps = new List<IPInfo>();

            foreach (var ipItem in ipItems)
            {
                try
                {
                    IPInfo updatedIPInfo = _IP2CService.getIPCountryDetails(ipItem.IP);
                    ipItem.UpdatedAt = updatedIPInfo.GenerationDate;

                    Country countryItem = await dbContext.Countries.FirstOrDefaultAsync(c => c.Name == updatedIPInfo.Country_Name);

                    if (countryItem == null)
                    {
                        countryItem = InfoSplit.ToCountry(updatedIPInfo);

                        dbContext.Countries.Add(countryItem);
                        dbContext.SaveChanges();
                    }

                    if (ipItem.CountryId != countryItem.Id)
                    {
                        ipItem.CountryId = countryItem.Id;
                        changedIps.Add(updatedIPInfo);
                    }
                }
                catch (IPNotFoundException e)
                {
                    dbContext.Remove(ipItem);
                    await dbContext.SaveChangesAsync();
                }


            }
            await dbContext.SaveChangesAsync();

            return changedIps;
        }

        public async Task<IEnumerable<CountryReportDTO>> GetIpReport(List<String>? twoLetterCodes)
        {
            string standardQuery = "SELECT c.Name AS CountryName, COUNT(i.IP) AS AddressesCount, MAX(UpdatedAt) AS LastAddressUpdated FROM Countries c INNER JOIN IPAddresses i ON c.Id = i.CountryId GROUP BY c.Name";
            string filteredQueryDapper = "SELECT c.Name AS CountryName, COUNT(i.IP) AS AddressesCount, MAX(UpdatedAt) AS LastAddressUpdated FROM Countries c INNER JOIN IPAddresses i ON c.Id = i.CountryId WHERE c.TwoLetterCode IN @twoLetterCodes GROUP BY c.Name";

            using (var connection = dbContext.Database.GetDbConnection())
            {
                if (twoLetterCodes.IsNullOrEmpty())
                    return await connection.QueryAsync<CountryReportDTO>(standardQuery);
                else 
                    return await connection.QueryAsync<CountryReportDTO>(filteredQueryDapper, new { twoLetterCodes });
            }


            //var querylinq = (from ips in dbContext.IPAddresses
            //                 join country in dbContext.Countries.Where(x => twoLetterCodes.Contains(x.TwoLetterCode))
            //                 on ips.CountryId equals country.Id
            //                 select new { name = country.Name, updated = ips.UpdatedAt })
            //                 .GroupBy(
            //                    name => name.name,
            //                    (name, obj) =>
            //                        new CountryReportDTO()
            //                        {
            //                            CountryName = name,
            //                            AddressesCount = obj.Count(),
            //                            LastAddressUpdated = obj.OrderByDescending(item => item.updated).First().updated
            //                        }
            //                    );
            //return querylinq;
        }

        public async Task<int> CountStoredIps() => await dbContext.IPAddresses.CountAsync();

    }


}
