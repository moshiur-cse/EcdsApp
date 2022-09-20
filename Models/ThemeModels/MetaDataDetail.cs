using EcdsApp.Models.TabularModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("tbl_metadata_details")]
    public class MetaDataDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("layer_id")]
        [Display(Name = "Layer")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }


        [Column("column_id")]
        [Display(Name = "Table Column Name")]
        public int? ColumnId { get; set; }
        [ForeignKey("ColumnId")]
        public virtual TableColumnInfo TableColumnInfo { get; set; }



        [Column("sub_layer")]
        [StringLength(1000)]
        [Display(Name = "Sub-Layer Name")]
        public string SubLayer { get; set; }

        [Required]
        [Column("title")]
        [StringLength(1000)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Column("abstract")]
        [StringLength(1000)]
        [Display(Name = "Abstract")]
        public string Abstract { get; set; }

        //[Required]
        [Column("general")]
        [StringLength(1000)]
        [Display(Name = "General")]
        public string General { get; set; }

        [Required]
        [Column("quality")]
        [StringLength(1000)]
        [Display(Name = "Quality and Extend")]
        public string Quality { get; set; }

        //[Required]
        [Column("completeness")]
        [StringLength(1000)]
        [Display(Name = "Completeness")]
        public string Completeness { get; set; }

        [Required]
        [Column("history_of_the_dataset")]
        [StringLength(1000)]
        [Display(Name = "History of the Dataset")]
        public string HistoryOfTheDataSet { get; set; }

        //[Required]
        [Column("purpose_of_production")]
        [StringLength(1000)]
        [Display(Name = "Purpose of Production")]
        public string PurposeOfProduction { get; set; }

        [Required]
        [Column("process_description")]
        [StringLength(1000)]
        [Display(Name = "Process Description")]
        public string ProcessDescription { get; set; }

        [Required]
        [Column("type_of_dataset")]
        [StringLength(1000)]
        [Display(Name = "Type of Dataset")]
        public string TypeOfDataSet { get; set; }

        [Required]
        [Column("dataset_language")]
        [StringLength(1000)]
        [Display(Name = "Dataset Language")]
        public string DataSetLanguage { get; set; }

        [Required]
        [Column("additional_info_source_for_dataset")]
        [StringLength(1000)]
        [Display(Name = "Additional Information")]
        public string AdditionalInfoSourceForDataSet { get; set; }
    }
}
