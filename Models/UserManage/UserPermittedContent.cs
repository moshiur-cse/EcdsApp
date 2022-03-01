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
        
        [Required]
        [Column("menu_name")]
        [StringLength(50)]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }
        
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
        public string MenuContent { get; set; }

        [Required]
        [Column("is_active")]
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
    }
}
