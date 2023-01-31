using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("respondents_perception_about_disaster")]
    public class RespondentsPerceptionaboutDisaster
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

        [Column("critical_sis_by_nature_or_Hman", TypeName = "int", Order = 2)]
        [Display(Name = "Critical Situation Created by Nature or Human")]
        public int CriticalSisByNatureOrHman { get; set; }

        [Column("usual_process_occurs_time_to_time", TypeName = "int", Order = 3)]
        [Display(Name = "Usual Process Occurs from Time to Time")]
        public int UsualProcessOccursTimeToTime { get; set; }

        [Column("happens_without_any_reason", TypeName = "int", Order = 4)]
        [Display(Name = "Happens Without Any Reason")]
        public int HappensWithoutAnyReason { get; set; }

        [Column("do_not_know", TypeName = "int", Order = 5)]
        [Display(Name = "Do Not Know")]
        public int DoNotKnow { get; set; }
    }
}
