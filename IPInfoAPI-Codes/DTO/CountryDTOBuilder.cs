namespace IPInfoAPI_Codes.DTO
{
    public class CountryDTOBuilder
    {
        private string CountryName;
        private string TwoLetterCode;
        private string ThreeLetterCode;

        public CountryDTOBuilder SetCountryName(string countryName)
        {
            this.CountryName = countryName;
            return this;
        }

        public CountryDTOBuilder SetTwoLetterCode(string twoLetterCode)
        {
            this.TwoLetterCode = twoLetterCode;
            return this;
        }

        public CountryDTOBuilder SetThreeLetterCode(string threeLetterCode)
        {
            this.ThreeLetterCode = threeLetterCode;
            return this;
        }

        public CountryDTO Build()
        {
            return new CountryDTO()
            {
                CountryName = CountryName,
                TwoLetterCode = TwoLetterCode,
                ThreeLetterCode = ThreeLetterCode
            };
        }
    }
}
