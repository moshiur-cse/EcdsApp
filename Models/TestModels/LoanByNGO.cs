using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcdsApp.Models.TestModels
{
    [Table("loan_by_ngos")]
    public class LoanByNGO
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

        [Column("Support_failure_during_or_after_disaster", TypeName = "decimal(10,2)")]
        [Display(Name = "Failed to have financial support during or after disaster")]
        public decimal? SupportFailureDuringOrAfterDisaster { get; set; }

        [Column("support_during_or_after_disaster", TypeName = "decimal(10,2)")]
        [Display(Name = "Financial support during or after disaster")]
        public decimal? SupportDuringOrAfterDisaster { get; set; }

        [Column("local_welfare_or_corporate_support", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from Local Society Welfare/Cooparative Socieity")]
        public decimal? LocalWelfareOrCorporateSupport { get; set; }

        [Column("support_from_business_enterprise", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from Business enterprises")]
        public decimal? SupportFromBusinessEnterprise { get; set; }

        [Column("support_from_local_persons", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from Local Elite Person")]
        public decimal? SupportFromLocalPersons { get; set; }

        [Column("support_from_int_orgs", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from International organizations")]
        public decimal? SupportFromIntOrg { get; set; }

        [Column("support_from_ngos", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from NGO’s")]
        public decimal? SupportFromNGOs { get; set; }

        [Column("support_from_gov_orgs", TypeName = "decimal(10,2)")]
        [Display(Name = "Got support from Goverment Organization")]
        public decimal? SupportFromGovOrgs { get; set; }

        [Column("support_from_others", TypeName = "decimal(10,2)")]
        [Display(Name = "Got spport from Others")]
        public decimal? SupportFromOthers { get; set; }
    }
}
