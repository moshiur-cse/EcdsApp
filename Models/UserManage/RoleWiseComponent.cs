using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EcdsApp.Models.ThemeModels;
using Microsoft.AspNetCore.Identity;

namespace EcdsApp.Models.UserManage
{
    [Table("user_role_wise_components")]
    public class RoleWiseComponent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public int Id { get; set; }

        [Column("role_id")]
        [Display(Name = "Role")]
        public string UserRoleId { get; set; }
        [ForeignKey("UserRoleId")]
        public IdentityRole UserRole { get; set; }

        [Column("component_id")]
        [Display(Name = "Component")]
        public int SubThemeId { get; set; }
        [ForeignKey("SubThemeId")]
        public virtual SubTheme SubTheme { get; set; }

        [Column("permitted_to_view")]
        public bool PermittedToView { get; set; }

        [Column("permitted_to_add_edit")]
        public bool PermittedToAddOrEdit { get; set; }

        [Column("permitted_to_delete")] 
        public bool PermittedToDelete { get; set; }

        [Column("permitted_to_download_data")]
        public bool PermittedToDownloadData { get; set; }
    }
}
