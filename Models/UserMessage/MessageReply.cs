using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.UserMessage
{
    [Table("tbl_message_replys")]
    public class MessageReply
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("replied_message", Order = 2, TypeName = "varchar(1000)")]
        [Display(Name = "Replied Message")]
        [StringLength(1000)]
        public string RepliedMsg { get; set; }

        [Required]
        [Column("message", Order = 3)]
        [Display(Name = "Message")]
        public int MsgId { get; set; }

        [ForeignKey("MsgId")]
        public virtual Message Message { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Created At")]
        [Column("created_at")]

        public DateTime CreatedAt { get; set; }
    }
}
