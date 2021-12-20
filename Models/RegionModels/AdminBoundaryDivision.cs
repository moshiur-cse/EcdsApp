using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models
{
    [Table("lkp_admin_boundary_divisions")]
    public class AdminBoundaryDivision
    {      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]       
        [Column("div_geo_code",Order = 0, TypeName = "varchar(2)")]
        [StringLength(2, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Division Geo-Code")]
        public string DivisionGeoCode { get; set; }


        //[Column("old_geo_code", Order = 0, TypeName = "varchar(2)")]
        //[StringLength(2, ErrorMessage = "The {0} must be {1} characters.")]
        //[Display(Name = "Old Geo-Code")]
        //public string OldGeoCode { get; set; }

        [Required]
        [Column("div_name", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Division Name")]
        public string DivisionName { get; set; }

        [Column("div_name_bangla", Order = 1, TypeName = "nvarchar(250)")]
        [StringLength(250)]
        [Display(Name = "Division Name(Bangla)")]
        public string DivisionNameBangla { get; set; }

        [Column("sorting_order", Order = 2, TypeName = "int")]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }

    }
}
