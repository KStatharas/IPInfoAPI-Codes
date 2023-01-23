using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPInfoAPI_Codes.Models
{
    public class IPAddress : IIPAddress
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column(TypeName = "varchar(15)")]
        [MaxLength(15)]
        public string IP { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
