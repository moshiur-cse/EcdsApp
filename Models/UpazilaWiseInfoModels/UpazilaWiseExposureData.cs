using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{
    [Table("tbl_upazila_wise_exposure_data")]
    public class UpazilaWiseExposureData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Column("id", Order = 0)]
        public int Id { get; set; }


        [Required]
        [Column("upz_geo_code", Order = 1, TypeName = "varchar(8)")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual AdminBoundaryUpazila Upazila { get; set; }


    }
}
