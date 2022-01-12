using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.TabularModels
{
    [Table("lkp_column_type")]
    public class ColumnType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("type_name")]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }
    }
}
