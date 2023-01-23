using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPInfoAPI_Codes.Models
{
    public class Country : ICountry
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<IPAddress> IPAddresses { get; set; }
    }
}
