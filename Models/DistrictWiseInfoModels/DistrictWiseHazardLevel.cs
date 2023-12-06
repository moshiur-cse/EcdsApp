using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("district_wise_hazard")]
    public class DistrictWiseHazardLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("dist_geo_code", Order = 2, TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("hazard_level_cyclone")]
        [Display(Name = "Hazard Level Cyclone")]
        public int HazardLevelCyclone { get; set; }

        [Column("hazard_level_drought_kharif")]
        [Display(Name = "Hazard Level Drought Kharif")]
        public int HazardLevelDroughtKharif { get; set; }

        [Column("hazard_level_drought_pre_kharif")]
        [Display(Name = "Hazard Level Drought Pre Kharif")]
        public int HazardLevelDroughtPreKharif { get; set; }

        [Column("hazard_level_earthquake")]
        [Display(Name = "Hazard Level Earthquake")]
        public int HazardLevelEarthquake { get; set; }

        [Column("hazard_level_erosion")]
        [Display(Name = "Hazard Level Erosion")]
        public int HazardLevelErosion { get; set; }

        [Column("hazard_level_flash_flood")]
        [Display(Name = "Hazard Level Flash Flood")]
        public int HazardLevelFlashFlood { get; set; }

        [Column("hazard_level_flood")]
        [Display(Name = "Hazard Level Flood")]
        public int HazardLevelFlood { get; set; }

        [Column("hazard_level_landslide")]
        [Display(Name = "Hazard Level Landslide")]
        public int HazardLevelLandslide { get; set; }

        [Column("hazard_level_salinity")]
        [Display(Name = "Hazard Level Salinity")]
        public int HazardLevelSalinity { get; set; }

        [Column("hazard_level_sea_level_rise")]
        [Display(Name = "Hazard Level Sea Level Rise")]
        public int HazardLevelSeaLevelRise { get; set; }

        [Column("hazard_level_storm_surge")]
        [Display(Name = "Hazard Level Storm Surge")]
        public int HazardLevelStormSurge { get; set; }


    }
}
