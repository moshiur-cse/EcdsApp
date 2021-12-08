using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("lkp_themes")]
    public class Theme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("theme_id", TypeName = "int")]
        [Display(Name = "Theme Id")]
        public int ThemeId { get; set; }

        [Required]
        [Column("theme_name")]
        [StringLength(256)]
        [Display(Name = "Theme Name")]
        public string ThemeName { get; set; }

        [Required]
        [Column("theme_path")]
        [StringLength(256)]
        [Display(Name = "Theme Path")]
        public string ThemePath { get; set; }        
    }
}
