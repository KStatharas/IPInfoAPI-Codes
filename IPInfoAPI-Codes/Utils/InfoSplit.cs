using IP2C_IPInfoProvider.Models;
using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Models;

namespace IPInfoAPI_Codes.Utils
{
    public class InfoSplit
    {
        public static IPAddress ToIPAddress(IPInfo ipInfoItem, int countryId)
        {
            return new IPAddress()
            {
                IP = ipInfoItem.IP,
                CountryId = countryId,
                CreatedAt = ipInfoItem.GenerationDate,
                UpdatedAt = ipInfoItem.GenerationDate
            };
        }

        public static Country ToCountry(IPInfo ipInfoItem)
        {
            return new Country()
            {
                Name = ipInfoItem.Country_Name,
                TwoLetterCode = ipInfoItem.TwoLetterCode,
                ThreeLetterCode = ipInfoItem.ThreeLetterCode,
                CreatedAt = ipInfoItem.GenerationDate

            };
        }

        public static CountryDTO ConvertToDTO (Country country)
        {
            return new CountryDTO()
            {
                Name = country.Name,
                TwoLetterCode = country.TwoLetterCode,
                ThreeLetterCode = country.ThreeLetterCode
            };
        }

        public static CountryDTO CreateCountryDTO (IPInfo ipInfoItem)
        {
            return new CountryDTO()
            {
                Name = ipInfoItem.Country_Name,
                TwoLetterCode = ipInfoItem.TwoLetterCode,
                ThreeLetterCode = ipInfoItem.ThreeLetterCode
            };
        }

    }
}
