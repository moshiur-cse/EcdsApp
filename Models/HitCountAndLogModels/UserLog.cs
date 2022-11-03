using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.HitCountAndLogModels
{
    public class UserLog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("log_type_id", Order = 2)]
        public int LogTypeId { get; set; }
        
        [ForeignKey("LogTypeId")]
        public virtual LogType LogType { get; set; }
        
        [Required]
        [Column("ip_address", Order = 3, TypeName = "varchar(100)")]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        
        [Required]
        [Column("generated_at", Order = 4)]
        [Display(Name = "Generated At")]
        public DateTime GeneratedAt { get; set; }
    }
}