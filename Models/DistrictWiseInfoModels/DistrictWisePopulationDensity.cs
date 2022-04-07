using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("tbl_district_wise_population_density")]
    public class DistrictWisePopulationDensity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("dist_geo_code", Order = 2, TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        //[ForeignKey("DistrictGeoCode")]
        // public virtual AdminBoundaryDistrict District { get; set; }

        [Column("population_density", TypeName = "int")]
        [Display(Name = "Population Density")]
        public int? PopulationDensity { get; set; }
    }
}
