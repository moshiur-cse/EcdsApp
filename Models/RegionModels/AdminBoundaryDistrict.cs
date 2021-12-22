using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models
{
    [Table("lkp_admin_boundary_districts")]
    public class AdminBoundaryDistrict
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("dist_geo_code", Order = 0, TypeName = "varchar(4)")]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }

        [Column("old_geo_code", Order = 0, TypeName = "varchar(4)")]
        [StringLength(4, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required]
        [Column("dist_name", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        //[Required]
        [Column("dist_name_bangla", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "District Name(Bangla)")]
        public string DistrictNameBangla { get; set; }


        [Column("div_geo_code",Order = 2, TypeName = "varchar(2)")]
        [StringLength(2)]
        [Display(Name = "Division Geo-Code")]
        public string DivisionGeoCode { get; set; }
        [ForeignKey("DivisionGeoCode")]
        public virtual AdminBoundaryDivision Division { get; set; }


        [Column("sorting_order", Order = 3, TypeName = "int")]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }

       




    }
}
