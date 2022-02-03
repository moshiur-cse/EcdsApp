using EcdsApp.Models.ThemeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models
{
    [Table("tbl_bundle_detatils")]
    public class BundleDetatil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("layer_id", Order = 1)]
        [Display(Name = "Layer Id")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }

        [Required]
        [Column("field_name", Order = 2, TypeName = "varchar(200)")]
        [StringLength(1000)]
        [Display(Name = "Field Name")]
        public string FieldName { get; set; }

        [Required]
        [Column("field_description", Order = 2, TypeName = "varchar(500)")]
        [StringLength(1000)]
        [Display(Name = "Field Name")]
        public string FieldDescription { get; set; }

        [Column("field_unit", Order = 2, TypeName = "varchar(50)")]
        [StringLength(1000)]
        [Display(Name = "Field Unit")]
        public string FieldUnit { get; set; }



    }
}
