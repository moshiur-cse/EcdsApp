using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("respondent_perception_about_disaster_management")]
    public class RespondentPerceptionAboutDisasterManagement
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

        [Column("to_reduce_loss_in_a_systematic_manner", TypeName = "int", Order = 2)]
        [Display(Name = "To Reduce Loss In a Systematic Manner")]
        public int ToReduceLossInASystematicManner { get; set; }

        [Column("assist_only_affected_people", TypeName = "int", Order = 3)]
        [Display(Name = "Assist Only Affected People")]
        public int AssistOnlyAffectedPeople { get; set; }

        [Column("to_stand_beside_the_affected_people", TypeName = "int", Order = 4)]
        [Display(Name = "To Stand Beside The Affected People")]
        public int ToStandBesideTheAffectedPeople { get; set; }

        [Column("do_not_know", TypeName = "int", Order = 5)]
        [Display(Name = "Do Not Know")]
        public int DoNotKnow { get; set; }
    }
}
