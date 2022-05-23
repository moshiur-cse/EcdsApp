using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.SystemCommon
{
  
        [Table("email_configurations")]
        public class EmailConfiguration
        {
            [Key]
            [Column("id")]
            public int Id { get; set; }

            [Required]
            [Column("sender_email", Order = 2, TypeName = "varchar(150)")]
            [Display(Name = "Sender Email")]
            public string SenderEmail { get; set; }

            [Required]
            [Column("category", Order = 3, TypeName = "varchar(150)")]
            [Display(Name = "Category")]
            public string Category { get; set; }

            [Required]
            [Column("host", Order = 4, TypeName = "varchar(150)")]
            [Display(Name = "Host")]
            public string Host { get; set; }

            [Required]
            [Column("port", Order = 5, TypeName = "varchar(150)")]
            [Display(Name = "Port")]
            public int port { get; set; }

            [Required]
            [Column("username", Order = 6, TypeName = "varchar(150)")]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [Column("password", Order = 7, TypeName = "varchar(150)")]
            [Display(Name = "Password")]
            public string Password { get; set; }
    }
}
