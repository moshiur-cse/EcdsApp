using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("tbl_district_wise_lightening")]
    public class DistrictWiseLightening
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }


        [Column("dist_geo_code", Order = 2, TypeName = "varchar(4)")]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        //[ForeignKey("DistrictGeoCode")]
        //public virtual AdminBoundaryDistrict District { get; set; }


        [Column("latitude", Order = 10, TypeName = "decimal(10, 8)")]
        [DataType(DataType.Text)]
        [Range(20.55555555, 26.66666666)]
        [Display(Name = "Latitude")]
        public decimal? Latitude { get; set; }

        [Column("longitude", Order = 11, TypeName = "decimal(10, 8)")]
        [DataType(DataType.Text)]
        [Range(88.00000000, 92.77777777)]
        [Display(Name = "Longitude")]
        public decimal? Longitude { get; set; }

        [Column("lightening")]
        [Display(Name = "Lightening")]
        public int? Lightening{ get; set; }
    }
}
