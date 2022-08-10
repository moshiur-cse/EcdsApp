using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.RegionModels
{
    [Table("lkp_admin_boundary_village")]
    public class AdminBoundaryVillage
    {
        [Key]
        [Required(ErrorMessage = "Geo Code field is mandatory")]
        [Column("village_geo_code", Order = 0, TypeName = "varchar(19)")]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "The value must be 15 characters.")]
        [Display(Name = "Village Geo-Code")]
        public string VillageGeoCode { get; set; }

        [Column("old_geo_code", Order = 1, TypeName = "varchar(19)")]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "The value must be 15 characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required(ErrorMessage = "Mauza Name field is mandatory")]
        [Column("village_name", Order = 2, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Village Name")]
        public string VillageName { get; set; }

        [Column("village_name_bangla", Order = 3, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Village Name (Bangla)")]
        public string VillageNameBangla { get; set; }

        [Required(ErrorMessage = "Mauza Geo Code field is mandatory")]
        [Column("mauza_geo_code", Order = 4, TypeName = "varchar(16)")]
        [StringLength(16, ErrorMessage = "The value must be {1} characters.")]
        [Display(Name = "Mauza Geo-Code")]
        public string MauzaGeoCode { get; set; }
        [ForeignKey("MauzaGeoCode")]
        public virtual AdminBoundaryMauza Mauza { get; set; }
    }
}
