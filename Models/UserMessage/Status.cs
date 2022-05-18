using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UserMessage
{
    [Table("tbl_reply_statuses")]
    public class Status
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Required]
        [Column("message_status", Order = 2, TypeName = "varchar(100)")]
        [Display(Name = "Message Status")]
        public string MessageStatus { get; set; }
    }
}
