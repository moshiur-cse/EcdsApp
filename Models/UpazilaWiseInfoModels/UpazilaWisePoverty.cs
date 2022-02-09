using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{

    [Table("tbl_upazila_wise_poverty")]
    public class UpazilaWisePoverty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("upz_geo_code")]
        [StringLength(8, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Upazila Geo-Code")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual AdminBoundaryUpazila Upazila { get; set; }

        [Column("Poverty_rate", TypeName = "decimal(5,2)")]
        [Display(Name = "Poverty Rate (%)")]
        public decimal? PovertyRate { get; set; }
    }
}
