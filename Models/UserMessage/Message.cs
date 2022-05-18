using EcdsApp.Models.UserMessage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models
{
    [Table("tbl_user_message")]
    public class Message
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("full_name", Order = 2, TypeName = "varchar(100)")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Column("email", Order = 3, TypeName = "varchar(100)")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Column("message", Order = 4, TypeName = "varchar(1000)")]
        [Display(Name = "Message")]
        public string Msg { get; set; }

        [Required]
        [Column("reply_status", Order = 5)]
        [Display(Name = "Reply Status")]
        public int ReplyStatusId { get; set; }

        [ForeignKey("ReplyStatusId")]
        public virtual Status Status { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Created At")]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
