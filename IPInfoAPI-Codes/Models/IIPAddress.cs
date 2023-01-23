namespace IPInfoAPI_Codes.Models
{
    public interface IIPAddress : IGenerationDate
    {
        public string IP { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
