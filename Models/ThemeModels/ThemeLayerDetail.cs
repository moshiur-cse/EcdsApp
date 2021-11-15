using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.ThemeModels
{
    [Table("tbl_theme_layer_details")]
    public class ThemeLayerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("layer_id", TypeName = "int")]
        [Display(Name = "Layer Id")]
        public int LayerId { get; set; }

        [Required]
        [Column("sub_theme_id", TypeName = "int")]
        public int SubThemeId { get; set; }
        [ForeignKey("SubThemeId")]
        public virtual SubTheme SubThemes { get; set; }

        [Required]
        [Column("layer_path")]
        [StringLength(256)]
        [Display(Name = "Layer Path")]
        public string LayerPath { get; set; }


        [Required]
        [Column("layer_name")]
        [StringLength(256)]
        [Display(Name = "Layer Name")]
        public string LayerName { get; set; }

        [Required]
        [Column("layer_file_name")]
        [StringLength(256)]
        [Display(Name = "Layer File Name")]
        public string LayerFileName { get; set; }


        [Required]
        [Column("layer_type_id", TypeName = "int")]
        public int LayerTypeId { get; set; }
        [ForeignKey("LayerTypeId")]
        public virtual ThemeLayerType ThemeLayerTypes { get; set; }


        [Column("layer_main_attribure_name")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure Name")]
        public string LayerMainAttribureName { get; set; }


        [Column("layer_main_attribure_one")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure One")]
        public string LayerMainAttribureOne { get; set; }

        [Column("layer_main_attribure_two")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure Two")]
        public string LayerMainAttribureTwo { get; set; }

        [Column("layer_main_attribure_three")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure Three")]
        public string LayerMainAttribureThree { get; set; }


        [Column("layer_main_attribure_code")]
        [StringLength(256)]
        [Display(Name = "Layer Main Attribure Code")]
        public string LayerMainAttribureCode { get; set; }

        [Column("file_lat_name")]
        [StringLength(256)]
        [Display(Name = "File Latitude Name")]
        public string FileLatName { get; set; }

        [Column("file_long_name")]
        [StringLength(256)]
        [Display(Name = "File Longitude Name")]
        public string FileLongName { get; set; }


        [Column("is_legend_color")]
        [Display(Name = "Is Legend Color")]
        public bool IsLegendColor { get; set; }





    }
}
