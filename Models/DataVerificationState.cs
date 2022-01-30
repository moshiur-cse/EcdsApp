using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models
{
    [Table("lkp_data_verification_states")]
    public class DataVerificationState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Column("state_name")]
        [StringLength(20)]
        [Display(Name = "State Name")]
        public string StateName { get; set; }
    }
}
