using System.Runtime.CompilerServices;

namespace IPInfoAPI_Codes.DTO
{
    public class CountryReportDTOBuilder
    {
        private string CountryName;
        private int AddressesCount;
        private DateTime LastAddressUpdated;


        public CountryReportDTOBuilder SetCountryName(string CountryName)
        {
            this.CountryName = CountryName;
            return this;
        }

        public CountryReportDTOBuilder SetAddressesCount(int AddressesCount)
        {
            this.AddressesCount = AddressesCount;
            return this;
        }

        public CountryReportDTOBuilder SetLastAddressUpdated(DateTime LastAddressUpdated)
        {
            this.LastAddressUpdated = LastAddressUpdated;
            return this;
        }


        public CountryReportDTO Build()
        {
            return new CountryReportDTO()
            {
                CountryName = CountryName,
                AddressesCount = AddressesCount,
                LastAddressUpdated = LastAddressUpdated
            };
        }
    }
}
