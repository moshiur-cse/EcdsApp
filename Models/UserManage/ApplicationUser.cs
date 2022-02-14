using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UserManage
{
    [Table("users")]
    public class ApplicationUser : IdentityUser
    {
        [Column("first_name")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        [Column("full_name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Column("date_of_birth")]
        [StringLength(20)]
        public string DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [Column("address")]
        [StringLength(500)]
        public string Address { get; set; }

        [Display(Name = "Mobile No")]
        [Column("mobile_no")]
        [StringLength(11)]
        public string MobileNo { get; set; }

        [Display(Name = "Organization")]
        [Column("organization")]
        [StringLength(100)]
        public string Organization { get; set; }

        [Display(Name = "Designation")]
        [Column("designation")]
        [StringLength(50)]
        public string Designation { get; set; }

        //[Required]
        [Column("user_activation_status_id")]
        [Display(Name = "User Activation Status Id")]
        public int? UserActivationStatusId { get; set; }

        [Column("date_of_creation")]
        [Display(Name = "Date Of Creation")]
        public DateTime? DateOfCreation { get; set; }

        [Column("last_modified_date")]
        [Display(Name = "Last Modified Date")]
        public DateTime? LastModifiedDate { get; set; }

        [Column("is_verified")]
        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }

        //[Required]
        [Column("verification_code")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }

        //[Required]
        [Column("user_role_id")]
        [Display(Name = "Role")]
        public string UserRoleId { get; set; }
        [ForeignKey("UserRoleId")]
        public IdentityRole UserRole { get; set; }
    }
}
