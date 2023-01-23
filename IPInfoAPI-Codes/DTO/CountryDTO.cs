namespace IPInfoAPI_Codes.DTO
{
    public class CountryDTO
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CountryDTO dTO &&
                   Name == dTO.Name &&
                   TwoLetterCode == dTO.TwoLetterCode &&
                   ThreeLetterCode == dTO.ThreeLetterCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, TwoLetterCode, ThreeLetterCode);
        }
    }
}
