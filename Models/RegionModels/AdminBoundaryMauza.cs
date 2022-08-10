using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.RegionModels
{
    [Table("lkp_admin_boundary_mauza")]
    public class AdminBoundaryMauza
    {
        [Key]
        [Required(ErrorMessage = "Geo Code field is mandatory")]
        [Column("mauza_geo_code", Order = 0, TypeName = "varchar(16)")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "The value must be 16 characters.")]
        [Display(Name = "Mauza Geo-Code")]
        public string MauzaGeoCode { get; set; }

        [Column("old_geo_code", Order = 1, TypeName = "varchar(16)")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "The value must be 15 characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required(ErrorMessage = "Mauza Name field is mandatory")]
        [Column("mauza_name", Order = 2, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Mauza Name")]
        public string MauzaName { get; set; }

        [Column("mauza_name_bangla", Order = 3, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Union Name (Bangla)")]
        public string MauzaNameBangla { get; set; }

        [Required(ErrorMessage = "Union Geo Code field is mandatory")]
        [Column("union_geo_code", Order = 4, TypeName = "varchar(13)")]
        [StringLength(13, ErrorMessage = "The value must be {1} characters.")]
        [Display(Name = "Union Geo-Code")]
        public string UnionGeoCode { get; set; }
        [ForeignKey("UnionGeoCode")]
        public virtual AdminBoundaryUnion Union { get; set; }

    }
}
