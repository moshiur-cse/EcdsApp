using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.UpazilaWiseInfoModels
{
    [Table("tbl_upazila_wise_literacy_rate")]
    public class UpazilaWiseLiteracyRate
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("upz_geo_code", TypeName = "varchar(8)")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual AdminBoundaryUpazila Upazila { get; set; }

        [Column("literacy_rate_both", TypeName = "int")]
        [Display(Name = "Literacy Rate Both")]
        public int? LiteracyRateBoth { get; set; }

        [Column("literacy_rate_male", TypeName = "int")]
        [Display(Name = "Literacy Rate Male")]
        public int? LiteracyRateMale { get; set; }

        [Column("literacy_rate_female", TypeName = "int")]
        [Display(Name = "Literacy Rate Female")]
        public int? LiteracyRateFemale { get; set; }


    }
}
