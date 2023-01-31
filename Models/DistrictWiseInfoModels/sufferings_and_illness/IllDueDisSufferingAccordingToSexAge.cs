using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("ill_due_dis_suffering_according_to_sex_age")]
    public class IllDueDisSufferingAccordingToSexAge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("dist_geo_code", TypeName = "varchar(4)", Order = 1)]
        [StringLength(4)]
        [Display(Name = "District Geo-Code")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual AdminBoundaryDistrict District { get; set; }

        [Column("total_household", TypeName = "int", Order = 2)]
        [Display(Name = "Total Household")]
        public int TotalHouseHold { get; set; }

        [Column("household_age_0_to_4", TypeName = "int", Order = 3)]
        [Display(Name = "Household (Age_0_to_4)")]
        public int HouseholdAge0to4 { get; set; }

        [Column("household_age_5_to_17", TypeName = "int", Order = 4)]
        [Display(Name = "Household (Age_5_to_17)")]
        public int HouseholdAge5to17 { get; set; }

        [Column("household_age_18_to_36", TypeName = "int", Order = 5)]
        [Display(Name = "Household (Age_18_to_36)")]
        public int HouseholdAge18to36 { get; set; }

        [Column("household_age_37_to_60", TypeName = "int", Order = 6)]
        [Display(Name = "Household (Age_37_to_60)")]
        public int HouseholdAge37to60 { get; set; }

        [Column("household_age_61_plus", TypeName = "int", Order = 7)]
        [Display(Name = "Household (Age_61+)")]
        public int HouseholdAge61Plus { get; set; }

        [Column("total_male", TypeName = "int", Order = 8)]
        [Display(Name = "Total Male")]
        public int TotalMale { get; set; }

        [Column("male_age_0_to_4", TypeName = "int", Order = 9)]
        [Display(Name = "Male (Age_0_to_4)")]
        public int MaleAge0to4 { get; set; }

        [Column("male_age_5_to_17", TypeName = "int", Order = 10)]
        [Display(Name = "Male (Age_5_to_17)")]
        public int MaleAge5to17 { get; set; }

        [Column("male_age_18_to_36", TypeName = "int", Order = 11)]
        [Display(Name = "Male (Age_18_to_36)")]
        public int MaleAge18to36 { get; set; }

        [Column("male_age_37_to_60", TypeName = "int", Order = 12)]
        [Display(Name = "Male (Age_37_to_60)")]
        public int MaleAge37to60 { get; set; }

        [Column("male_age_61_plus", TypeName = "int", Order = 13)]
        [Display(Name = "Male (Age_61+)")]
        public int MaleAge61Plus { get; set; }

        [Column("total_female", TypeName = "int", Order = 14)]
        [Display(Name = "Total Female")]
        public int TotalFemale { get; set; }

        [Column("female_age_0_to_4", TypeName = "int", Order = 15)]
        [Display(Name = "Female (Age_0_to_4)")]
        public int FemaleAge0to4 { get; set; }

        [Column("female_age_5_to_17", TypeName = "int", Order = 16)]
        [Display(Name = "female (Age_5_to_17)")]
        public int FemaleAge5to17 { get; set; }

        [Column("female_age_18_to_36", TypeName = "int", Order = 17)]
        [Display(Name = "Female (Age_18_to_36)")]
        public int FemaleAge18to36 { get; set; }

        [Column("female_age_37_to_60", TypeName = "int", Order = 18)]
        [Display(Name = "Female (Age_37_to_60)")]
        public int FemaleAge37to60 { get; set; }

        [Column("female_age_61_plus", TypeName = "int", Order = 19)]
        [Display(Name = "Female (Age_61+)")]
        public int FemaleAge61Plus { get; set; }

    }
}
