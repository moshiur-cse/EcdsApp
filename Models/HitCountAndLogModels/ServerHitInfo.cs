using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.HitCountAndLogModels
{
    public class ServerHitInfo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("ip_address", Order = 2, TypeName = "varchar(100)")]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        
        [Column("country_of_origin", Order = 4, TypeName = "varchar(100)")]
        [Display(Name = "Country of Origin")]
        public string? CountryOfOrigin { get; set; }
        
        [Required]
        [Column("requested_at", Order = 3)]
        [Display(Name = "Requested At")]
        public DateTime RequestedAt { get; set; }
    }
}