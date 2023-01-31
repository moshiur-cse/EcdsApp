using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcdsApp.Models.DistrictWiseInfoModels
{
    [Table("trtment_faclti_rcv_by_hh_insufficient_wtr_sply")]
    public class TrtmentFacltiRcvByHHInsufficientWtrSply
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

        [Column("do_not_do_anything", TypeName = "int", Order = 2)]
        [Display(Name = "Do Not Do Anything")]
        public int DoNotDoAnything { get; set; }

        [Column("self_treatment", TypeName = "int", Order = 3)]
        [Display(Name = "Self Treatment")]
        public int SelfTreatment { get; set; }

        [Column("medicine_shop_or_pharmacy", TypeName = "int", Order = 4)]
        [Display(Name = "Medicine Shop Or Pharmacy")]
        public int MedicineShopOrPharmacy { get; set; }

        [Column("kobiraj_or_ohja", TypeName = "int", Order = 5)]
        [Display(Name = "Kobiraj Or Ohja")]
        public int KobirajOrOhja { get; set; }

        [Column("mbbs_doctor", TypeName = "int", Order = 6)]
        [Display(Name = "MBBS Doctor")]
        public int MbbsDoctor { get; set; }

        [Column("district_government_hospital", TypeName = "int", Order = 7)]
        [Display(Name = "District Government Hospital")]
        public int DistrictGovernmentHospital { get; set; }

        [Column("upazila_health_n_family_welfare_clinic", TypeName = "int", Order = 8)]
        [Display(Name = "Upazila Health And Family Welfare Clinic")]
        public int UpazilaHealthAndFamilyWelfareClinic { get; set; }

        [Column("union_health_n_family_welfare_clinic", TypeName = "int", Order = 9)]
        [Display(Name = "Union Health And Family Welfare Clinic")]
        public int UnionHealthAndFamilyWelfareClinic { get; set; }

        [Column("community_clinic", TypeName = "int", Order = 10)]
        [Display(Name = "Community Clinic")]
        public int CommunityClinic { get; set; }

        [Column("village_doctor", TypeName = "int", Order = 11)]
        [Display(Name = "Village Doctor")]
        public int VillageDoctor { get; set; }

        [Column("homeo_doctor", TypeName = "int", Order = 12)]
        [Display(Name = "Homeo Doctor")]
        public int HomeoDoctor { get; set; }

        [Column("others", TypeName = "int", Order = 13)]
        [Display(Name = "Others")]
        public int Others { get; set; }


    }
}
