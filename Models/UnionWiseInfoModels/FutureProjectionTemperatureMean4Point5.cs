using EcdsApp.Models.RegionModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.UnionWiseInfoModels
{
    [Table("tbl_future_projection_temperature_mean_4_point_5")]
    public class FutureProjectionTemperatureMean4Point5
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("union_geo_code")]
        [StringLength(13, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UnionGeoCode { get; set; }
        [ForeignKey("UnionGeoCode")]
        public virtual AdminBoundaryUnion Unions { get; set; }


        [Column("year_2020_2039", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2020 To 2039")]
        public decimal? Year2020To2039 { get; set; }

        [Column("year_2040_2059", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2040 To 2059")]
        public decimal? Year2040To2059 { get; set; }

        [Column("year_2060_2079", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2060 To 2079")]
        public decimal? Year2060To2079 { get; set; }

        [Column("year_2080_2099", TypeName = "decimal(5,2)")]
        [Display(Name = "Year 2080 To 2099")]
        public decimal? Year2080To2099 { get; set; }
    }
}
