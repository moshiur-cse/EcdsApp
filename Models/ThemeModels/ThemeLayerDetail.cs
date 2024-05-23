using EcdsApp.Models.TabularModels;
using EcdsApp.Models.UserManage;
using System;
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
        [Display(Name = "Sub Theme")]
        public int SubThemeId { get; set; }
        [ForeignKey("SubThemeId")]
        public virtual SubTheme SubThemes { get; set; }

        //Layer Name
        [Required]
        [Column("layer_name")]
        [StringLength(100)]
        [Display(Name = "Layer Name")]
        public string LayerName { get; set; }

        //Layer Display Name
        [Required]
        [Column("layer_display_name")]
        [StringLength(100)]
        [Display(Name = "Layer Display Name")]
        public string LayerDisplayName { get; set; }

        //[Required]
        [Column("layer_file_name")]
        [StringLength(256)]
        [Display(Name = "File Name")]
        public string LayerFileName { get; set; }

        [Required]
        [Column("layer_type_id", TypeName = "int")]
        [Display(Name = "Layer Type")]
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

        [Column("file_lat_name")]
        [StringLength(256)]
        [Display(Name = "File Latitude Name")]
        public string FileLatName { get; set; }

        [Column("file_long_name")]
        [StringLength(256)]
        [Display(Name = "File Longitude Name")]
        public string FileLongName { get; set; }

        [Column("boundary_info_id")]
        [Display(Name = "Regional Boundary")]
        public int? BoundaryInfoId { get; set; }
        [ForeignKey("BoundaryInfoId")]
        public virtual BoundaryInfo BoundaryInfo { get; set; }

        [Column("table_info_id")]
        [Display(Name = "Table Name")]
        public int? TableInfoId { get; set; }
        [ForeignKey("TableInfoId")]
        public virtual TableInfo TableInfo { get; set; }

        [Column("legend_color_option_id")]
        [Display(Name = "Legend Color")]
        public int? LegendColorOptionId { get; set; }
        [ForeignKey("LegendColorOptionId")]
        public virtual LegendColorOption LegendColorOption { get; set; }

        [Column("legend_color_field_name")]
        [StringLength(256)]
        [Display(Name = "Legend Color Field Name")]
        public string LegendColorFieldName { get; set; }

        [Required]
        [Column("line_color_code")]
        [StringLength(10)]
        [Display(Name = "Line Color Code")]
        public string LineColorCode { get; set; }

        [Column("fill_color_code")]
        [StringLength(10)]
        [Display(Name = "Fill Color Code")]
        public string FillColorCode { get; set; }

        [Required]
        [Column("opacity", TypeName = "decimal(4, 2)")]
        [Range(0.0, 1.1)]
        [Display(Name = "Opacity")]
        public decimal Opacity { get; set; }

        [Required]
        [Column("fill_opacity", TypeName = "decimal(4, 2)")]
        [Range(0.0, 1.1)]
        [Display(Name = "Fill Opacity")]
        public decimal FillOpacity { get; set; }

        [Required]
        [Column("line_weight", TypeName = "decimal(4, 2)")]
        [Range(0.0, 10)]
        [Display(Name = "Line Weight")]
        public decimal LineWeight { get; set; }

        [Column("data_verification_state")]
        [Display(Name = "Verification State")]
        public int? DataVerificationStateId { get; set; }
        [ForeignKey("DataVerificationStateId")]
        public virtual DataVerificationState DataVerificationState { get; set; }

        [Column("sorting_order")]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }


        [Column("user_id")]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Column("generated_at")]
        [Display(Name = "Generated At")]
        public DateTime? GeneratedAt { get; set; }

        [Column("read_status")]
        [Display(Name = "Read Status")]
        public bool ReadStatus { get; set; }

    }
}
