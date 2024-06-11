using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.RegionModels
{
    [Table("lkp_admin_boundary_unions")]
    public class AdminBoundaryUnion
    {
        [Key]
        [Required(ErrorMessage = "Union Geo Code field is mandatory")]
        [Column("union_geo_code", Order = 0, TypeName = "varchar(13)")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "The value must be 13 characters.")]
        [Display(Name = "Union Geo-Code")]
        public string UnionGeoCode { get; set; }

        [Column("old_geo_code", Order = 1, TypeName = "varchar(10)")]
        [StringLength(10,MinimumLength =10, ErrorMessage = "The value must be 10 characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required(ErrorMessage = "Union Name field is mandatory")]
        [Column("union_name", Order = 2, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Union Name")]
        public string UnionName { get; set; }

        //[Required]
        [Column("union_name_bangla", Order = 3, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Union Name (Bangla)")]
        public string UnionNameBangla { get; set; }

        [Required(ErrorMessage = "Upazila Geo Code field is mandatory")]
        [Column("upz_geo_code")]
        //[StringLength(8, ErrorMessage = "The value must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual AdminBoundaryUpazila Upazila { get; set; }


        [Column("municipality_geo_code", Order = 4, TypeName = "varchar(10)")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The value must be 10 characters.")]
        [Display(Name = "Municipality Geo-Code")]
        public string MunicipalityGeoCode { get; set; }

        [Column("municipality_name", Order = 5, TypeName = "nvarchar(250)")]
        [StringLength(250,ErrorMessage = "The value must be less than 250 characters.")]
        [Display(Name = "Municipality Name")]
        public string MunicipalityName { get; set; }


        [Column("sorting_order", Order =6, TypeName = "int")]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }
    }
}
