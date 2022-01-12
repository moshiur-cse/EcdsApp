using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{
    [Table("tbl_upazila_wise_risk_index")]
    public class UpazilaWiseRiskIndex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("upz_geo_code")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual AdminBoundaryUpazila Upazila { get; set; }

        [Column("people_affected_natural_disaster", TypeName = "decimal(5,2)")]
        //[RegularExpression(@"^(0|-?\d{0,5}(\.\d{0,2})?)$")]
        public decimal? PeopleAffectedNaturalDisaster { get; set; }

        [Column("heat_stress_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? HeatStressVulnerability { get; set; }

        [Column("ground_water_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? GroundWaterVulnerability { get; set; }

        [Column("mangrove_forest_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? MangroveForestVulnerability { get; set; }

        [Column("live_stock_land_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? LivestockLandVulnerability { get; set; }

        [Column("water_availability_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? WaterAvailabilityVulnerability { get; set; }

        [Column("crop_yield_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? CropYieldVulnerability { get; set; }

        [Column("livestock_health_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? LivestockHealthVulnerability { get; set; }

        [Column("agri_land_availability_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? AgriLandAvailabilityVulnerability { get; set; }
        
        [Column("fish_culture_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? FishCultureVulnerability { get; set; }

        [Column("fish_capture_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? FishCaptureVulnerability { get; set; }

        [Column("rail_network_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? RailNetworkVulnerability { get; set; }

        [Column("road_network_vulnerability", TypeName = "decimal(5,2)")]
        public decimal? RoadNetworkVulnerability { get; set; }

    }
}
