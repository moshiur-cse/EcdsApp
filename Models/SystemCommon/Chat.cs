using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.SystemCommon
{
    [Table("chats")]
    [Index(nameof(Sender), IsUnique = false)]
    [Index(nameof(Receiver), IsUnique = false)]
    public class Chat
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("sender_email", Order = 2, TypeName = "varchar(150)")]
        [Display(Name = "Sender Email")]
        public string Sender { get; set; }

        [Required]
        [Column("message", Order = 3, TypeName = "varchar(500)")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Column("receiver_email", Order = 4, TypeName = "varchar(150)")]
        [Display(Name = "Receiver Email")]
        public string Receiver { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}")]
        [Column("sent_at")]
        [Display(Name = "Sent At")]
        public DateTime SentAt { get; set; }
    }
}
