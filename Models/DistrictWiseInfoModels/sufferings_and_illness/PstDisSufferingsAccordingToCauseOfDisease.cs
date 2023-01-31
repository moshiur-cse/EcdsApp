using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("pst_dis_sufferings_according_to_cause_of_disease")]
    public class PstDisSufferingsAccordingToCauseOfDisease
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

        [Column("temperature_variation", TypeName = "int", Order = 2)]
        [Display(Name = "Temperature Variation")]
        public int TemperatureVariation { get; set; }

        [Column("variation_in_rain", TypeName = "int", Order = 3)]
        [Display(Name = "Variation In Rain")]
        public int VariationInRain { get; set; }

        [Column("water_pollution", TypeName = "int", Order = 4)]
        [Display(Name = "Water Pollution")]
        public int WaterPollution { get; set; }

        [Column("air_pollution", TypeName = "int", Order = 5)]
        [Display(Name = "Air Pollution")]
        public int AirPollution { get; set; }

        [Column("unplanned_sanitation", TypeName = "int", Order = 6)]
        [Display(Name = "Unplanned Sanitation")]
        public int UnplannedSanitation { get; set; }

        [Column("during_disaster", TypeName = "int", Order = 7)]
        [Display(Name = "During Disaster")]
        public int DuringDisaster { get; set; }

        [Column("not_known", TypeName = "int", Order = 8)]
        [Display(Name = "Not Known")]
        public int NotKnown { get; set; }

        [Column("others", TypeName = "int", Order = 9)]
        [Display(Name = "Others")]
        public int Others { get; set; }
    }
}
