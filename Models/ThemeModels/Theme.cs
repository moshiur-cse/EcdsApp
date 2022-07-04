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

        [Column("theme_color")]
        [StringLength(10)]
        [Display(Name = "Theme Color")]
        public string ThemeColor { get; set; }

        [Column("theme_short_name")]
        [StringLength(30)]
        [Display(Name = "Theme Short Name")]
        public string ThemeShortName { get; set; }

        [Column("theme_description")]
        [StringLength(200)]
        [Display(Name = "Theme Description")]
        public string ThemeDescription { get; set; }

        [Column("theme_icon")]
        [StringLength(200)]
        [Display(Name = "Theme Icon")]
        public string ThemeIcon { get; set; }

        [Required]
        [Column("theme_path")]
        [StringLength(256)]
        [Display(Name = "Theme Path")]
        public string ThemePath { get; set; }
    }
}
