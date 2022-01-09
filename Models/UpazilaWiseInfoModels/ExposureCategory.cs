using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UpazilaWiseInfoModels
{
    [Table("lkp_exposure_category")]
    public class ExposureCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("category_name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
