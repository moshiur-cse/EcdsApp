using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("dist_wise_loss_and_damage_of_agriculture")]
    public class DistWiseLossAndDamageOfAgriculture
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


        [Column("paddy", TypeName = "decimal(10,2)")]
        [Display(Name = "Paddy")]
        public decimal? Paddy { get; set; }

        [Column("potato", TypeName = "decimal(10,2)")]
        [Display(Name = "Potato")]
        public decimal? Potato { get; set; }

        [Column("wheat", TypeName = "decimal(10,2)")]
        [Display(Name = "Wheat")]
        public decimal? Wheat { get; set; }

        [Column("jute", TypeName = "decimal(10,2)")]
        [Display(Name = "Jute")]
        public decimal? Jute { get; set; }

        [Column("pulse", TypeName = "decimal(10,2)")]
        [Display(Name = "Pulse")]
        public decimal? Pulse { get; set; }

        [Column("fruits", TypeName = "decimal(10,2)")]
        [Display(Name = "Fruits")]
        public decimal? Fruits { get; set; }

        [Column("other_crop", TypeName = "decimal(10,2)")]
        [Display(Name = "Other Crop")]
        public decimal? OtherCrop { get; set; }

        [Column("livestock", TypeName = "decimal(10,2)")]
        [Display(Name = "Livestock")]
        public decimal? Livestock { get; set; }

        [Column("poultry_birds", TypeName = "decimal(10,2)")]
        [Display(Name = "Poultry/Birds")]
        public decimal? PoultryOrBirds { get; set; }

        [Column("fisheries", TypeName = "decimal(10,2)")]
        [Display(Name = "Fisheries")]
        public decimal? Fisheries { get; set; }

        [Column("others", TypeName = "decimal(10,2)")]
        [Display(Name = "Others")]
        public decimal? Others { get; set; }

    }
}
