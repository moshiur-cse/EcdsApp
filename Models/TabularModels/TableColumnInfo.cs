using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.TabularModels
{
    [Table("tbl_table_column_info")]
    public class TableColumnInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("table_id")]
        [Display(Name = "Table")]
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public virtual TableInfo TableInfo { get; set; }

        [Required]
        [Column("db_column_name")]
        [StringLength(50)]
        [Display(Name = "DB Column")]
        public string DbColumnName { get; set; }

        [Required]
        [Column("model_property_name")]
        [StringLength(50)]
        [Display(Name = "Model Property Name")]
        public string ModelPropertyName { get; set; }

        [Required]
        [Column("display_name")]
        [StringLength(50)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Column("column_type_id")]
        [Display(Name = "Column Type")]
        public int? ColumnTypeId { get; set; }
        [ForeignKey("ColumnTypeId")]
        public virtual ColumnType ColumnType { get; set; }
    }
}
