using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("respondent_perception_about_impact_climate_cng")]
    public class RespondentPerceptionAboutImpactOfClimateChange
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

        [Column("sea_level_rise", TypeName = "int", Order = 2)]
        [Display(Name = "Sea Level Rise")]
        public int SeaLevelRise { get; set; }

        [Column("drought_or_dryness", TypeName = "int", Order = 3)]
        [Display(Name = "Drought or Dryness")]
        public int DroughtOrDryness { get; set; }

        [Column("flood_or_water_logging", TypeName = "int", Order = 4)]
        [Display(Name = "Flood or Water Logging")]
        public int FloodOrWaterLogging { get; set; }

        [Column("salinity", TypeName = "int", Order = 5)]
        [Display(Name = "Salinity")]
        public int Salinity { get; set; }

        [Column("storm_or_tornado_or_hailstorm", TypeName = "int", Order = 6)]
        [Display(Name = "Storm or Tornado or Hailstorm")]
        public int StormOrTornadoOrHailstorm { get; set; }

        [Column("tidal_surge_or_cyclone_or_hurricane", TypeName = "int", Order = 7)]
        [Display(Name = "Tidal Surge or Cyclone or Hurricane")]
        public int TidalSurgeOrCycloneOrHurricane { get; set; }

        [Column("do_not_know", TypeName = "int", Order = 8)]
        [Display(Name = "Do Not Know")]
        public int DoNotKnow { get; set; }
    }
}
