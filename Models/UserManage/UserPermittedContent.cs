using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UserManage
{
    [Table("user_permitted_contents")]
    public class UserPermittedContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("content_id")]
        [Display(Name = "Content Id")]
        public int ContentId { get; set; }

        [Column("theme_id")]
        [Display(Name = "Themes")]
        public string ThemeId { get; set; }

        [Required]
        [Column("sub_theme_id")]
        [Display(Name = "Sub-Themes")]
        public string SubThemeId { get; set; }

        [Required]
        [Column("layer_id")]
        [Display(Name = "Layers")]
        public string LayerId { get; set; }

        [Required]
        [Column("module_id")]
        [Display(Name = "Module Id")]
        public int? ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual UserAccessModule UserAccessModule { get; set; }

        [Required]
        [Column("menu_name")]
        [StringLength(50)]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        [Required]
        [Column("submenu_name")]
        [StringLength(50)]
        [Display(Name = "Sub Menu")]
        public string SubMenuName { get; set; }

        [Required]
        [Column("controller_name")]
        [StringLength(50)]
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Required]
        [Column("action_name")]
        [StringLength(50)]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Required]
        [Column("submenu_content")]
        [StringLength(50)]
        [Display(Name = "SubMenu Content")]
        public string SubMenuContent { get; set; }

        [Required]
        [Column("is_active")]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Required]
        [Column("is_menu_item")]
        [Display(Name = "Menu Item?")]
        public bool IsMenuItem { get; set; }

        [Required]
        [Column("is_disabled")]
        [Display(Name = "Is Disabled?")]
        public bool IsDisabled { get; set; }
    }
}
