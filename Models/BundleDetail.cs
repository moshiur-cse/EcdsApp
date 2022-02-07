using EcdsApp.Models.ThemeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models
{
    [Table("tbl_bundle_details")]
    public class BundleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("layer_id")]
        [Display(Name = "Layer")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }

        [Required]
        [Column("field_name")]
        [StringLength(200)]
        [Display(Name = "Field Name")]
        public string FieldName { get; set; }

        [Required]
        [Column("field_description")]
        [StringLength(500)]
        [Display(Name = "Field Description")]
        public string FieldDescription { get; set; }

        [Column("field_unit")]
        [StringLength(50)]
        [Display(Name = "Field Unit")]
        public string FieldUnit { get; set; }
    }
}
