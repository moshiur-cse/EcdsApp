using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DivisionWiseInfoModels
{
    [Table("household_affected_by_non_working_days")]
    public class HouseholdAffectedByNonWorkingDays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("div_geo_code", TypeName = "varchar(2)", Order = 1)]
        [StringLength(2)]
        [Display(Name = "Division Geo-Code")]
        public string DivisionGeoCode { get; set; }
        [ForeignKey("DivisionGeoCode")]
        public virtual AdminBoundaryDivision Division { get; set; }

        [Column("day_1_to_7", TypeName = "int", Order = 2)]
        [Display(Name = "1 - 7")]
        public int Day1To7 { get; set; }

        [Column("day_8_to_15", TypeName = "int", Order = 3)]
        [Display(Name = "8 - 15")]
        public int Day8To15 { get; set; }

        [Column("day_16_to_30", TypeName = "int", Order = 4)]
        [Display(Name = "16 - 30")]
        public int Day16To30 { get; set; }

        [Column("day_31_to_45", TypeName = "int", Order = 5)]
        [Display(Name = "31 - 45")]
        public int Day31To45 { get; set; }

        [Column("day_46_to_60", TypeName = "int", Order = 6)]
        [Display(Name = "46 - 60")]
        public int Day46To60 { get; set; }

        [Column("day_61_plus", TypeName = "int", Order = 7)]
        [Display(Name = "61 +")]
        public int Day61Plus { get; set; }
    }
}
