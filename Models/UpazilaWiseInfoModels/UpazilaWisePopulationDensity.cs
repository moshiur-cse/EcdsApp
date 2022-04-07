using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{

    [Table("tbl_upazila_wise_population_density")]
    public class UpazilaWisePopulationDensity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("upz_geo_code", TypeName = "varchar(8)")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        //[ForeignKey("UpazilaGeoCode")]
        //public virtual AdminBoundaryUpazila Upazila { get; set; }

        [Column("population_density", TypeName = "int")]
        [Display(Name = "Population Density")]
        public int? PopulationDensity { get; set; }
    }
}
