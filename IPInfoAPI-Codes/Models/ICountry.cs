namespace IPInfoAPI_Codes.Models
{
    public interface ICountry : IGenerationDate
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
    }
}
