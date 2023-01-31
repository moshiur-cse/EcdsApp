using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("bfor_dis_sufferings_according_to_type_of_disease")]
    public class BforDisSufferingsAccordingToTypeOfDisease
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

        [Column("diarrhoea", TypeName = "int", Order = 2)]
        [Display(Name = "Diarrhoea")]
        public int Diarrhoea { get; set; }

        [Column("dysentery", TypeName = "int", Order = 3)]
        [Display(Name = "Dysentery")]
        public int Dysentery { get; set; }

        [Column("malaria", TypeName = "int", Order = 4)]
        [Display(Name = "Malaria")]
        public int Malaria { get; set; }


        [Column("skin_disease", TypeName = "int", Order = 5)]
        [Display(Name = "Skin Disease")]
        public int SkinDisease { get; set; }

        [Column("cold_or_cough", TypeName = "int", Order = 6)]
        [Display(Name = "Cold Or Cough")]
        public int ColdOrCough { get; set; }

        [Column("fever", TypeName = "int", Order = 7)]
        [Display(Name = "Fever")]
        public int Fever { get; set; }

        [Column("typhoid", TypeName = "int", Order = 8)]
        [Display(Name = "typhoid")]
        public int Typhoid { get; set; }

        [Column("asthma", TypeName = "int", Order = 9)]
        [Display(Name = "Asthma")]
        public int Asthma { get; set; }

        [Column("jaundice", TypeName = "int", Order = 10)]
        [Display(Name = "Jaundice")]
        public int Jaundice { get; set; }

        [Column("malnutrition_related", TypeName = "int", Order = 11)]
        [Display(Name = "Malnutrition Related")]
        public int MalnutritionRelated { get; set; }

        [Column("dengue", TypeName = "int", Order = 12)]
        [Display(Name = "Dengue")]
        public int Dengue { get; set; }

        [Column("chikungunia", TypeName = "int", Order = 13)]
        [Display(Name = "Chikungunia")]
        public int Chikungunia { get; set; }

        [Column("mental_disorder", TypeName = "int", Order = 14)]
        [Display(Name = "Mental Disorder")]
        public int MentalDisorder { get; set; }

        [Column("chicken_pox", TypeName = "int", Order = 15)]
        [Display(Name = "Chicken Pox")]
        public int ChickenPox { get; set; }

        [Column("cholera", TypeName = "int", Order = 16)]
        [Display(Name = "Cholera")]
        public int Cholera { get; set; }

        [Column("others", TypeName = "int", Order = 17)]
        [Display(Name = "Others")]
        public int Others { get; set; }
    }
}
