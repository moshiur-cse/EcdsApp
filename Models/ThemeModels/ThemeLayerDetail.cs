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

        //Main Attribute
        [Column("main_attribute_display_name")]
        [StringLength(256)]
        [Display(Name = "Main Attribute Display Name")]
        public string MainAttributeDisplayName { get; set; }

        [Column("main_attribute_name")]
        [StringLength(256)]
        [Display(Name = "Main Attribute Name")]
        public string MainAttributeName { get; set; }

        [Column("main_attribute_code")]
        [StringLength(256)]
        [Display(Name = "Main Attribute Code")]
        public string MainAttributeCode { get; set; }


        //First Attribute
        [Column("first_attribute_display_name")]
        [StringLength(256)]
        [Display(Name = "First Attribute Display Name")]
        public string FirstAttributeDisplayName { get; set; }

        [Column("first_attribute_name")]
        [StringLength(256)]
        [Display(Name = "First Attribute Name")]
        public string FirstAttributeName { get; set; }

        [Column("first_attribute_code")]
        [StringLength(256)]
        [Display(Name = "First Attribute Code")]
        public string FirstAttributeCode { get; set; }


        //Second Attribute
        [Column("second_attribute_display_name")]
        [StringLength(256)]
        [Display(Name = "Second Attribute Display Name")]
        public string SecondAttributeDisplayName { get; set; }

        [Column("second_attribute_name")]
        [StringLength(256)]
        [Display(Name = "Second Attribute Name")]
        public string SecondAttributeName { get; set; }

        [Column("second_attribute_code")]
        [StringLength(256)]
        [Display(Name = "Second Attribute Code")]
        public string SecondAttributeCode { get; set; }


        //Third Attribute
        [Column("third_attribute_display_name")]
        [StringLength(256)]
        [Display(Name = "Third Attribute Display Name")]
        public string ThirdAttributeDisplayName { get; set; }

        [Column("third_attribute_name")]
        [StringLength(256)]
        [Display(Name = "Third Attribute Name")]
        public string ThirdAttributeName { get; set; }

        [Column("third_attribute_code")]
        [StringLength(256)]
        [Display(Name = "Third Attribute Code")]
        public string ThirdAttributeCode { get; set; }



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


        [Column("legend_color_field_name")]
        [StringLength(256)]
        [Display(Name = "Legend Color Field Name")]
        public string LegendColorFieldName { get; set; }




    }
}
