using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("drinking_water_by_source_dur_disaster")]
    public class DrinkingWaterBySourceDurDisaster
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

        [Column("pipe_or_tap", TypeName = "int", Order = 2)]
        [Display(Name = "Pipe/Tap(Wasa/Municipality)")]
        public int PipeOrTap { get; set; }

        [Column("shallow_tube_well_199_ft", TypeName = "int", Order = 3)]
        [Display(Name = "Shallow Tube Well(upto 199feets)")]
        public int ShallowTubeWell199Ft { get; set; }


        [Column("deep_tube_well_200_ft_or_more", TypeName = "int", Order = 4)]
        [Display(Name = "Deep Tube Well(200 feets or more)")]
        public int DeepTubeWell200FtOrMore { get; set; }

        [Column("pond_or_dighi", TypeName = "int", Order = 5)]
        [Display(Name = "Pond Or Dighi")]
        public int PondOrDighi { get; set; }

        [Column("canel_or_river", TypeName = "int", Order = 6)]
        [Display(Name = "Canel Or River")]
        public int CanelOrRiver { get; set; }

        [Column("rainfall_or_water_fall", TypeName = "int", Order = 7)]
        [Display(Name = "Rainfall Or Waterfall")]
        public int RainfallOrWaterFall { get; set; }

        [Column("well", TypeName = "int", Order = 8)]
        [Display(Name = "Well")]
        public int Well { get; set; }

        [Column("bottle_water", TypeName = "int", Order = 9)]
        [Display(Name = "Bottle Water")]
        public int BottleWater { get; set; }

        [Column("others", TypeName = "int", Order = 10)]
        [Display(Name = "Others")]
        public int Others { get; set; }
    }
}
