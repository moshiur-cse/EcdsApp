using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.UserManage;

namespace EcdsApp.Models.HitCountAndLogModels
{

    [Table("download_log")]
    public class DownloadLog
    {
        [Key] [Column("id")] 
        public int Id { get; set; }


        [Column("user_id")] 
        public string UserId { get; set; }
        [ForeignKey("UserId")] 
        public virtual ApplicationUser User { get; set; }


        [Required]
        [Column("ip_address", TypeName = "varchar(100)")]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Required]
        [Column("generated_at")]
        [Display(Name = "Generated At")]
        public DateTime GeneratedAt { get; set; }

        [Required] [Column("theme_layer_id")] public int ThemeLayerId { get; set; }

        [ForeignKey("ThemeLayerId")] public virtual ThemeLayerDetail ThemeLayerDetail { get; set; }
    }
}