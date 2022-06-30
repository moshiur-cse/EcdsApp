using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("district_wise_fertility_rate_cpr")]
    public class DistrictWiseFertilityRateCPR
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

        [Column("year_2016", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2016 (%)")]
        public decimal? Year2016 { get; set; }

        [Column("year_2017", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2017 (%)")]
        public decimal? Year2017 { get; set; }

        [Column("year_2018", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2018 (%)")]
        public decimal? Year2018 { get; set; }

        [Column("year_2019", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2019 (%)")]
        public decimal? Year2019 { get; set; }

        [Column("year_2020", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2020 (%)")]
        public decimal? Year2020 { get; set; }
    }
}
