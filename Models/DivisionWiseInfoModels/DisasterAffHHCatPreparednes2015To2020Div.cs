using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcdsApp.Models.DivisionWiseInfoModels
{
    [Table("disaster_aff_hh_cat_preparednes_2015_to_2020_div")]
    public class DisasterAffHHCatPreparednes2015To2020Div
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

        [Column("brick_built", TypeName = "int", Order = 2)]
        [Display(Name = "Brick Built")]
        public int BrickBuilt { get; set; }

        [Column("semi_brick_built", TypeName = "int", Order = 3)]
        [Display(Name = "Semi Brick Built")]
        public int SemiBrickBuilt { get; set; }

        [Column("strengthen_infrastructure", TypeName = "int", Order = 4)]
        [Display(Name = "Strengthen Infrastructure")]
        public int StrengthenInfrastructure { get; set; }

        [Column("tube_well_for_drinking_water", TypeName = "int", Order = 5)]
        [Display(Name = "Tube Well For Drinking Water")]
        public int TubeWellForDrinkingWater { get; set; }

        [Column("improved_sanitation", TypeName = "int", Order = 6)]
        [Display(Name = "Improved Sanitation")]
        public int ImprovedSanitation { get; set; }

        [Column("raise_road_for_communication", TypeName = "int", Order = 7)]
        [Display(Name = "Raise Road For Communication")]
        public int RaiseRoadForCommunication { get; set; }

        [Column("send_school_going_children_to_safe_place", TypeName = "int", Order = 8)]
        [Display(Name = "Send School-Going Children To Safe Place")]
        public int SendSchoolGoingChildrenToSafePlace { get; set; }

        [Column("increased_security_for_family_food_storage", TypeName = "int", Order = 9)]
        [Display(Name = "Increased Security For Family Food Storage")]
        public int IncreasedSecurityForFamilyFoodStorage { get; set; }

        [Column("raised_house", TypeName = "int", Order = 10)]
        [Display(Name = "Raised House")]
        public int RaisedHouse { get; set; }

        [Column("others", TypeName = "int", Order = 11)]
        [Display(Name = "Others")]
        public int Others { get; set; }
    }
}
