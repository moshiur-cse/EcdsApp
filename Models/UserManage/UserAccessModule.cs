using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UserManage
{
    [Table("user_access_modules")]
    public class UserAccessModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("module_id")]
        [Display(Name = "Module Id")]
        public int ModuleId { get; set; }

        [Required]
        [Column("module_name")]
        [StringLength(250)]
        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }
    }
}
