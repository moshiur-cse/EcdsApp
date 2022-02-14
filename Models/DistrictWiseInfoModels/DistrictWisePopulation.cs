using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("tbl_district_wise_population")]
    public class DistrictWisePopulation
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

        [Column("male", TypeName = "int")]
        [Display(Name = "MALE")]
        public int Male { get; set; }

        [Column("female", TypeName = "int")]
        [Display(Name = "FEMALE")]
        public int Female { get; set; }

        [Column("urban", TypeName = "int")]
        [Display(Name = "URBAN")]
        public int Urban { get; set; }

        [Column("rural", TypeName = "int")]
        [Display(Name = "RURAL")]
        public int Rural { get; set; }

        [Column("other", TypeName = "int")]
        [Display(Name = "OTHER")]
        public int Other { get; set; }

        [Column("total", TypeName = "int")]
        [Display(Name = "TOTAL")]
        public int Total { get; set; }

    }
}
