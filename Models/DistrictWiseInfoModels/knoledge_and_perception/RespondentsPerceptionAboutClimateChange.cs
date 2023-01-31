using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("respondents_perception_about_climate_change")]
    public class RespondentsPerceptionAboutClimateChange
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

        [Column("ltc_due_to_natural_or_human_act", TypeName = "int", Order = 2)]
        [Display(Name = "Long-term changes Due to Natural or Human Activities")]
        public int LTCDueToNaturalOrHumanAct { get; set; }

        [Column("reg_variation_in_temp_and_rainfall", TypeName = "int", Order = 3)]
        [Display(Name = "Regional Variation in Temperature and Rainfall")]
        public int RegVariationInTempAndRainfall { get; set; }

        [Column("ext_evnt_cus_loss_human_life_n_struc", TypeName = "int", Order = 4)]
        [Display(Name = "Extreme Events Causing Loss Of Human Life And Infrastructure")]
        public int ExtEvntCusLossHumanLifeNStruc { get; set; }

        [Column("others", TypeName = "int", Order = 5)]
        [Display(Name = "Others")]
        public int Others { get; set; }

        [Column("do_not_know", TypeName = "int", Order = 6)]
        [Display(Name = "Do Not Know")]
        public int DoNotKnow { get; set; }
    }
}
