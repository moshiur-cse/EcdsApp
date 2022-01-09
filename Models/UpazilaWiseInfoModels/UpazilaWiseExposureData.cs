using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{
    [Table("tbl_upazila_wise_exposure_data")]
    public class UpazilaWiseExposureData
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

        [Column("flood_value")]
        [Display(Name = "Flood")]
        public int? FloodValue { get; set; }
        [ForeignKey("FloodValue")]
        public virtual ExposureCategory FloodExposure { get; set; }

        [Column("storm_surge_value")]
        [Display(Name = "Storm Surge")]
        public int? StormSurgeValue { get; set; }
        [ForeignKey("StormSurgeValue")]
        public virtual ExposureCategory StormSurgeExposure { get; set; }

        [Column("land_slide_value")]
        [Display(Name = "Land Slide")]
        public int? LandSlideValue { get; set; }
        [ForeignKey("LandSlideValue")]
        public virtual ExposureCategory LandSlideExposure { get; set; }

        [Column("drought_value")]
        [Display(Name = "Drought")]
        public int? DroughtValue { get; set; }
        [ForeignKey("DroughtValue")]
        public virtual ExposureCategory DroughtExposure { get; set; }

        [Column("earthquake_value")]
        [Display(Name = "Earthquake")]
        public int? EarthquakeValue { get; set; }
        [ForeignKey("EarthquakeValue")]
        public virtual ExposureCategory EarthquakeExposure { get; set; }

        [Column("tsunami_value")]
        [Display(Name = "Tsunami")]
        public int? TsunamiValue { get; set; }
        [ForeignKey("TsunamiValue")]
        public virtual ExposureCategory TsunamiExposure { get; set; }
    }
}
