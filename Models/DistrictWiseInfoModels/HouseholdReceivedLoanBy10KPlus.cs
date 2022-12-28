using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("household_received_loan_by_10k_plus")]
    public class HouseholdReceivedLoanBy10KPlus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("dist_geo_code", Order = 2, TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("tk_1_to_9999", TypeName = "decimal(10,2)")]
        [Display(Name = "1 - 9999 Tk")]
        public decimal? Tk1To9999 { get; set; }

        [Column("tk_10k_to_24999", TypeName = "decimal(10,2)")]
        [Display(Name = "10000 - 24999 Tk")]
        public decimal? Tk10kTo24999 { get; set; }

        [Column("tk_25k_to_49999", TypeName = "decimal(10,2)")]
        [Display(Name = "25000 - 49999 Tk")]
        public decimal? Tk25kTo49999 { get; set; }

        [Column("tk_50k_to_99999", TypeName = "decimal(10,2)")]
        [Display(Name = "50000 - 99999 Tk")]
        public decimal? Tk50kTo99999 { get; set; }

        [Column("tk_100k_plus", TypeName = "decimal(10,2)")]
        [Display(Name = "50000 - 99999 Tk")]
        public decimal? Tk100kPlus { get; set; }
    }
}
