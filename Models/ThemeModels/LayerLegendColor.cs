using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Display(Name = "Layer Legend Color Id")]
        public int LayerLegendColorId { get; set; }

        [Column("layer_id", TypeName = "int")]
        [Display(Name = "Layer Id")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }

        //[Column("layer_main_attribure_code")]
        //[StringLength(20)]
        //[Display(Name = "Layer Main Attribure Code")]
        //public string LayerMainAttribureCode { get; set; }

        [Column("layer_main_attribure_value")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure Value")]
        public string LayerMainAttribureValue { get; set; }

        [Required]
        [Column("layer_legend_color_code")]
        [StringLength(20)]
        [Display(Name = "Layer Legend Color Code")]
        public string LayerLegendColorCode { get; set; }
    }
}
