using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EcdsApp.Models.UserManage
{
    [Table("user_role_wise_permitted_contents")]
    public class RoleWisePermittedContent
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

        [Column("content_id")]
        [Display(Name = "Content")]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public virtual UserPermittedContent UserPermittedContent { get; set; }
    }
}
