using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.ThemeModels
{
    [Table("tbl_metadata_details")]
    public class MetaDataDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("layer_id", Order = 1)]
        [Display(Name = "Layer Id")]
        public int LayerId { get; set; }
        [ForeignKey("LayerId")]
        public virtual ThemeLayerDetail ThemeLayerDetails { get; set; }


        [Required]
        [Column("title", Order = 2, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Title")]
        public string Title { get; set; }


        [Required]
        [Column("abstract", Order = 3, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Abstract")]
        public string Abstract { get; set; }


        [Required]
        [Column("general", Order = 4, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "General")]
        public string General { get; set; }


        [Required]
        [Column("quality", Order = 5, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Quality")]
        public string Quality { get; set; }


        [Required]
        [Column("completeness", Order = 6, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Completeness")]
        public string Completeness { get; set; }


        [Required]
        [Column("history_of_the_dataset", Order = 7, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "History of the dataset")]
        public string HistoryOfTheDataset { get; set; }


        [Required]
        [Column("purpose_of_production", Order = 8, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Purpose of production")]
        public string PurposeOfProduction { get; set; }


        [Required]
        [Column("process_description", Order = 9, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Process description")]
        public string ProcessDescription { get; set; }


        [Required]
        [Column("type_of_dataset", Order = 10, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Type of dataset")]
        public string TypeOfDataset { get; set; }


        [Required]
        [Column("dataset_language", Order = 11, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Dataset Language")]
        public string DatasetLanguage { get; set; }


        [Required]
        [Column("additional_info_source_for_dataset", Order = 12, TypeName = "varchar(1000)")]
        [StringLength(1000)]
        [Display(Name = "Additional information source for the dataset")]
        public string AdditionalInfoSourceForDataset { get; set; }


    }
}
