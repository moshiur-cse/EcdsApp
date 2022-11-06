using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.HitCountAndLogModels
{
    public class LogType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("log_type", Order = 2, TypeName = "varchar(100)")]
        [Display(Name = "Log Type")]
        public string Type { get; set; }
        
    }
}