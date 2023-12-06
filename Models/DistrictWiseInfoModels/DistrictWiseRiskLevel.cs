using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    public class DistrictWiseRiskLevel
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

        [Column("risk_level_cyclone")]
        [Display(Name = "Risk Level Cyclone")]
        public int RiskLevelCyclone { get; set; }

        [Column("risk_level_drought_kharif")]
        [Display(Name = "Risk Level Drought Kharif")]
        public int RiskLevelDroughtKharif { get; set; }

        [Column("risk_level_drought_pre_kharif")]
        [Display(Name = "Risk Level Drought Pre Kharif")]
        public int RiskLevelDroughtPreKharif { get; set; }

        [Column("risk_level_earthquake")]
        [Display(Name = "RiskL Level Earthquake")]
        public int RiskLevelEarthquake { get; set; }

        [Column("risk_level_erosion")]
        [Display(Name = "Risk Level Erosion")]
        public int RiskLevelErosion { get; set; }

        [Column("risk_level_flash_flood")]
        [Display(Name = "Risk Level Flash Flood")]
        public int RiskLevelFlashFlood { get; set; }

        [Column("risk_level_flood")]
        [Display(Name = "Risk Level Flood")]
        public int RiskLevelFlood { get; set; }

        [Column("risk_level_landslide")]
        [Display(Name = "Risk Level Landslide")]
        public int RiskLevelLandslide { get; set; }

        [Column("risk_level_salinity")]
        [Display(Name = "Risk Level Salinity")]
        public int RiskLevelSalinity { get; set; }

        [Column("risk_level_sea_level_rise")]
        [Display(Name = "Risk Level Sea Level Rise")]
        public int RiskLevelSeaLevelRise { get; set; }

        [Column("risk_level_storm_surge")]
        [Display(Name = "Risk Level Storm Surge")]
        public int RiskLevelStormSurge { get; set; }
    }
}
