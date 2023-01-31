using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("disaster_aff_n_preparednes_hh_2015_to_2020_dist")]
    public class DisasterAffNPreparednesHH2015To2020Dist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("dist_geo_code", TypeName = "varchar(4)", Order = 1)]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("household_preparedness", TypeName = "int", Order = 2)]
        [Display(Name = "Household Preparedness")]
        public int HouseholdPreparedness { get; set; }

        [Column("household_not_preparedness", TypeName = "int", Order = 3)]
        [Display(Name = "Household Not Preparedness")]
        public int HouseholdNotPreparedness { get; set; }
    }
}
