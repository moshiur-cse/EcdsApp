using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("lkp_theme_layer_types")]
    public class ThemeLayerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("layer_type_id", TypeName = "int")]
        [Display(Name = "Layer Type Id")]
        public int LayerTypeId { get; set; }

        [Required]
        [Column("Layer_type_name")]
        [StringLength(256)]
        [Display(Name = "Layer Type Name")]
        public string LayerTypeName { get; set; }
    }
}
