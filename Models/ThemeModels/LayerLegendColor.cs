using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("tbl_layer_legend_colors")]
    public class LayerLegendColor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("layer_legend_color_id", TypeName = "int")]
        [Display(Name = "Legend Color Id")]
        public int LayerLegendColorId { get; set; }

        [Column("layer_id", TypeName = "int")]
        [Display(Name = "Layer Id")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }

        [Column("layer_main_attribure_value")]
        [StringLength(256)]
        [Display(Name = "Main Attribute Value")]
        public string LayerMainAttribureValue { get; set; }

        [Column("layer_legend_color_code")]
        [StringLength(10)]
        [Display(Name = "Legend Color Code")]
        public string LayerLegendColorCode { get; set; }

        [Column("layer_legend_display_name")]
        [StringLength(200)]
        [Display(Name = "Legend Display Name")]
        public string LayerLegendDisplayName { get; set; }

        [Column("icon_size", TypeName = "decimal(4, 2)")]
        [Range(0.0, 100)]
        [Display(Name = "Icon Size")]
        public decimal IconSize { get; set; } = 0;

        [Column("icon_path")]
        [StringLength(200)]
        [Display(Name = "Icon Path")]
        public string IconPath { get; set; }

        [Column("legend_column_name")]
        [StringLength(200)]
        [Display(Name = "Legend Column Name")]
        public string LegendColumnName { get; set; }


    }
}
