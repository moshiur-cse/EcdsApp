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
        [Display(Name = "Boundary Name")]
        public string BoundaryName { get; set; }
    }
}
