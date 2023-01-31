using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("ill_due_dis_not_suffering_according_to_sex")]
    public class IllDueDisNotSufferingAccordingToSex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("dist_geo_code", TypeName = "varchar(4)", Order = 1)]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("total_population", TypeName = "int", Order = 2)]
        [Display(Name = "Total Population")]
        public int TotalPopulation { get; set; }

        [Column("male", TypeName = "int", Order = 3)]
        [Display(Name = "Male")]
        public int Male { get; set; }

        [Column("female", TypeName = "int", Order = 4)]
        [Display(Name = "Female")]
        public int Female { get; set; }
    }
}
