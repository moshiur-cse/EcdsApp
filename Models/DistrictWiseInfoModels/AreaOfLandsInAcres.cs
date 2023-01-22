using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("area_of_lands_in_acres")]
    public class AreaOfLandsInAcres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("dist_geo_code", TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("homestead", TypeName = "decimal(10,2)")]
        [Display(Name = "Homestead")]
        public decimal? Homestead { get; set; }

        [Column("garden_nursery", TypeName = "decimal(10,2)")]
        [Display(Name = "Garden Nursery")]
        public decimal? GardenNursery { get; set; }

        [Column("crop_land", TypeName = "decimal(10,2)")]
        [Display(Name = "CropLand")]
        public decimal? CropLand { get; set; }

        [Column("pond/wetland", TypeName = "decimal(10,2)")]
        [Display(Name = "Pond/WetLand")]
        public decimal? PondOrWetLand { get; set; }

        [Column("waste_or_otherland", TypeName = "decimal(10,2)")]
        [Display(Name = "Waste/OtherLand")]
        public decimal? WasteOrOtherLand { get; set; }
    }
}
