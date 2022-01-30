using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcdsApp.Models.ThemeModels;

namespace EcdsApp.Models.TabularModels
{
    [Table("tbl_table_info")]
    public class TableInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("sub_theme_id")]
        [Display(Name = "Sub Theme")]
        public int SubThemeId { get; set; }
        [ForeignKey("SubThemeId")]
        public virtual SubTheme SubThemes { get; set; }

        [Column("boundary_id")]
        [Display(Name = "Regional Boundary")]
        public int? BoundaryId { get; set; }
        [ForeignKey("BoundaryId")]
        public virtual BoundaryInfo BoundaryInfo { get; set; }

        [Required]
        [Column("table_name")]
        [StringLength(50)]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Required]
        [Column("table_model_name")]
        [StringLength(50)]
        [Display(Name = "Table Model Name")]
        public string TableModelName { get; set; }

        [Required]
        [Column("display_name")]
        [StringLength(50)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
