using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("lkp_legend_color_option")]
    public class LegendColorOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Column("option_name")]
        [StringLength(50)]
        [Display(Name = "Option Name")]
        public string OptionName { get; set; }
    }
}
