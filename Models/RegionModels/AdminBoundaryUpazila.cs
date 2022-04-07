using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models
{
    [Table("lkp_admin_boundary_upazilas")]
    public class AdminBoundaryUpazila
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column("upz_geo_code", Order = 0, TypeName = "varchar(8)")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }




        [Column("old_geo_code", Order = 0, TypeName = "varchar(6)")]
        [StringLength(6, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required]
        [Column("upz_name", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Upazila Name")]
        public string UpazilaName { get; set; }

        [Column("upz_name_bangla", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Upazila Name (Bangla)")]
        public string UpazilaNameBangla { get; set; }

        [Column("city_geo_code", Order = 0, TypeName = "varchar(6)")]
        [StringLength(6, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "City Geo-Code")]
        public string CityGeoCode { get; set; }

        [Column("city_name", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Column("dist_geo_code", Order = 2, TypeName = "varchar(4)")]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("sorting_order", Order = 3, TypeName = "int")]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}
