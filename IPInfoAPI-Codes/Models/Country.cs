using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPInfoAPI_Codes.Models
{
    public class Country : ICountry
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("TwoLetterCode")]
        public string TwoLetterCode { get; set; }
        [Column("ThreeLetterCode")]
        public string ThreeLetterCode { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        public List<IPAddress> IPAddresses { get; set; }
    }
}
