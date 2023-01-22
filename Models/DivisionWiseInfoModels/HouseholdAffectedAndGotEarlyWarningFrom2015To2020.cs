using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DivisionWiseInfoModels
{
    [Table("household_affected_and_got_early_warning_from_2015_to_2020")]
    public class HouseholdAffectedAndGotEarlyWarningFrom2015To2020
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

        [Column("affected_household", TypeName = "int", Order = 2)]
        [Display(Name = "Affected Household")]
        public int AffectedHousehold { get; set; }

        [Column("got_early_warning", TypeName = "int", Order = 3)]
        [Display(Name = "Got Early Warning")]
        public int GotEarlyWarning { get; set; }
    }





}
