using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DivisionWiseInfoModels
{
    [Table("household_got_early_warning_by_type_of_media")]
    public class HouseholdGotEarlyWarningByTypeOfMedia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("div_geo_code", TypeName = "varchar(2)", Order = 1)]
        [StringLength(2)]
        [Display(Name = "Division Geo-Code")]
        public string DivisionGeoCode { get; set; }
        [ForeignKey("DivisionGeoCode")]
        public virtual AdminBoundaryDivision Division { get; set; }

        [Column("radio", TypeName = "int", Order = 2)]
        [Display(Name = "Radio")]
        public int Radio { get; set; }

        [Column("television", TypeName = "int", Order = 3)]
        [Display(Name = "Television")]
        public int Television { get; set; }

        [Column("making", TypeName = "int", Order = 4)]
        [Display(Name = "Making")]
        public int Making { get; set; }

        [Column("community", TypeName = "int", Order = 5)]
        [Display(Name = "Community")]
        public int Community { get; set; }

        [Column("local_administration", TypeName = "int", Order = 6)]
        [Display(Name = "Local Administration")]
        public int LocalAdministration { get; set; }

        [Column("mobile_telephone_or_sms", TypeName = "int", Order = 7)]
        [Display(Name = "Mobile Telephone/SMS")]
        public int MobileTelephoneOrSMS { get; set; }


        [Column("internet_media", TypeName = "int", Order = 8)]
        [Display(Name = "Internet Media")]
        public int InternetMedia { get; set; }


        [Column("others", TypeName = "int", Order = 9)]
        [Display(Name = "Others")]
        public int Others { get; set; }

    }
}


