using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    
    [Table("loss_and_damage_of_properties")]
    public class LossAndDamageOfProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("dist_geo_code",TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("dwelling_house", TypeName = "decimal(10,2)")]
        [Display(Name = "Dwelling House")]
        public decimal? DwellingHouse { get; set; }

        [Column("kitchen_cowshed", TypeName = "decimal(10,2)")]
        [Display(Name = "Kitchen/Cowshed")]
        public decimal? Kitchen_Cowshed { get; set; }

        [Column("market_shop", TypeName = "decimal(10,2)")]
        [Display(Name = "Market/Shop")]
        public decimal? MarketShop { get; set; }

        [Column("tubewell_and_other", TypeName = "decimal(10,2)")]
        [Display(Name = "Tubewell & Other")]
        public decimal? TubewellAndOther { get; set; }

        [Column("homestead_forestry", TypeName = "decimal(10,2)")]
        [Display(Name = "Homestead/Forestry")]
        public decimal? HomesteadForestry { get; set; }
    }
}
