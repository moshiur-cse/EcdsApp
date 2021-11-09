using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.UserManage
{
    [Table("users")]
    public class UserRegistration : IdentityUser
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column("UserRegistrationId", Order = 0, TypeName = "int")]
        //[DataType(DataType.Text)]
        //[Display(Name = "User Registration Id")]
        //public int UserRegistrationId { get; set; }



        //[Required]
        [DataType(DataType.Text)]
        [Column("user_activation_status_id", Order = 1)]
        [Display(Name = "User Activation Status Id")]
        public int? UserActivationStatusId { get; set; }

        [Column("date_of_creation", Order = 2)]
        [Display(Name = "Date Of Creation")]
        public DateTime? DateOfCreation { get; set; }

        [Column("last_modified_date", Order = 3)]
        [Display(Name = "Last Modified Date")]
        public DateTime? LastModifiedDate { get; set; }

        [Column("is_verified", Order = 4)]
        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }

        //[Required]
        [Column("verification_code", Order = 3)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }

    }
}
