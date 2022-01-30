using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.TabularModels
{
    [Table("lkp_boundary_info")]
    public class BoundaryInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("boundary_name")]
        [StringLength(50)]
        [Display(Name = "Boundary Name")]
        public string BoundaryName { get; set; }

        [Column("attribute_name")]
        [StringLength(50)]
        [Display(Name = "Attribute Name")]
        public string AttributeName { get; set; }

        [Column("boundary_path")]
        [StringLength(100)]
        [Display(Name = "Boundary Path")]
        public string BoundaryPath { get; set; }
    }
}
