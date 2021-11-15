using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("lkp_sub_themes")]
    public class SubTheme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("sub_theme_id", TypeName = "int")]
        [Display(Name = "Sub Theme Id")]
        public int SubThemeId { get; set; }

        [Required]
        [Column("theme_id", TypeName = "int")]
        public int ThemeId { get; set; }
        [ForeignKey("ThemeId")]
        public virtual Theme Themes { get; set; }

        [Required]
        [Column("sub_theme_name")]
        [StringLength(256)]
        [Display(Name = "Sub Theme Name")]
        public string SubThemeName { get; set; }

        [Required]
        [Column("sub_theme_path")]
        [StringLength(256)]
        [Display(Name = "Sub Theme Path")]
        public string SubThemePath { get; set; }
    }
}
