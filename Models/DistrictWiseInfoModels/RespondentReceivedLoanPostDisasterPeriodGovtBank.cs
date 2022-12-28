using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("respondent_received_loan_govt_bank")]
    public class RespondentReceivedLoanPostDisasterPeriodGovtBank
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

        [Column("not_received_loan", TypeName = "decimal(10,2)")]
        [Display(Name = "Not received loan")]
        public decimal? NotReceivedLoan { get; set; }

        [Column("local_society", TypeName = "decimal(10,2)")]
        [Display(Name = "Local Society")]
        public decimal? LocalSociety { get; set; }

        [Column("welfare_cooparative_society", TypeName = "decimal(10,2)")]
        [Display(Name = "Welfare/Cooparative Society")]
        public decimal? WelfareCooparativeSociety { get; set; }

        [Column("local_mahajan", TypeName = "decimal(10,2)")]
        [Display(Name = "Local Mahajan")]
        public decimal? LocalMahajan { get; set; }


        [Column("relatives", TypeName = "decimal(10,2)")]
        [Display(Name = "Relatives")]
        public decimal? Relatives { get; set; }

        [Column("private_micro_loan", TypeName = "decimal(10,2)")]
        [Display(Name = "Private Micro Loan")]
        public decimal? PrivateMicroLoan { get; set; }

        [Column("govt_bank", TypeName = "decimal(10,2)")]
        [Display(Name = "Govt. Bank")]
        public decimal? GovtBank { get; set; }

        [Column("private_bank", TypeName = "decimal(10,2)")]
        [Display(Name = "Private Bank")]
        public decimal? PrivateBank { get; set; }

        [Column("others", TypeName = "decimal(10,2)")]
        [Display(Name = "Others")]
        public decimal? Others { get; set; }


    }
}
