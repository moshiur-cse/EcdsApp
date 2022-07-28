using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    public class DistrictWiseMotherHealthRisk
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


        [Column("ant_care1", TypeName = "decimal(5,2)")]
        public decimal? AntCare1 { get; set; }

        [Column("ant_care4", TypeName = "decimal(5,2)")]
        public decimal? AntCare4 { get; set; }

        [Column("ant_care_ub", TypeName = "decimal(5,2)")]
        public decimal? AntCareUB { get; set; }

        [Column("neo_tetanus", TypeName = "decimal(5,2)")]
        public decimal? NeoTetanus { get; set; }

        [Column("inst_deliv", TypeName = "decimal(5,2)")]
        public decimal? InstDeliv { get; set; }

        [Column("caesar", TypeName = "decimal(5,2)")]
        public decimal? Caesar { get; set; }

        [Column("pn_health", TypeName = "decimal(5,2)")]
        public decimal? PnHealth { get; set; }



    }
}
