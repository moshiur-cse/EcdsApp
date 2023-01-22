using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("disaster_affected_household_from_2015_to_2020")]
    public class DisasterAffectedHouseholdFrom2015To2020
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

        [Column("drought", TypeName = "int", Order = 2)]
        [Display(Name = "Drought")]
        public int Drought { get; set; }

        [Column("flood", TypeName = "int", Order = 3)]
        [Display(Name = "Flood")]
        public int Flood { get; set; }

        [Column("water_logging", TypeName = "int", Order = 4)]
        [Display(Name = "Water Logging")]
        public int WaterLogging { get; set; }

        [Column("cyclone", TypeName = "int", Order = 5)]
        [Display(Name = "Cyclone")]
        public int Cyclone { get; set; }

        [Column("tornado", TypeName = "int", Order = 6)]
        [Display(Name = "Tornado")]
        public int Tornado { get; set; }

        [Column("strom_or_tridal_surge", TypeName = "int", Order = 7)]
        [Display(Name = "Strom/Tridal Surge")]
        public int StromOrTridalSurge { get; set; }

        [Column("thunderstrom_or_lightning", TypeName = "int", Order = 8)]
        [Display(Name = "Thunderstrom/Lightning")]
        public int ThunderstromOrLightning { get; set; }

        [Column("river_or_coastal_erosion", TypeName = "int", Order = 9)]
        [Display(Name = "River/Coastal Erosion")]
        public int RiverOrCoastalErosion { get; set; }

        [Column("landslide", TypeName = "int", Order = 10)]
        [Display(Name = "Landslide")]
        public int Landslide { get; set; }

        [Column("salinity", TypeName = "int", Order = 10)]
        [Display(Name = "Salinity")]
        public int Salinity { get; set; }

        [Column("hailstrom", TypeName = "int", Order = 11)]
        [Display(Name = "Hailstrom")]
        public int Hailstrom { get; set; }

        [Column("other_disasters", TypeName = "int", Order = 12)]
        [Display(Name = "Other Disasters")]
        public int OtherDisasters { get; set; }
    }
}
