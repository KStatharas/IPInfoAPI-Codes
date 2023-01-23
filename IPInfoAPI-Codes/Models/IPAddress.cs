using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPInfoAPI_Codes.Models
{
    public class IPAddress : IIPAddress
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("IP",TypeName = "varchar(15)")]
        [MaxLength(15)]
        public string IP { get; set; }
        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Country")]
        [Column("CountryId")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
