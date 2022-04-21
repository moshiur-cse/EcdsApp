using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EcdsApp.Models
{
    [Table("lkp_admin_boundary_divisions")]
    public class AdminBoundaryDivision
    {      
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("div_geo_code")]
        [StringLength(2, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Division Geo-Code")]
        public string DivisionGeoCode { get; set; }


        [Column("old_geo_code")]
        [StringLength(2, ErrorMessage = "The {0} must be {1} characters.")]
        [Display(Name = "Old Geo-Code")]
        public string OldGeoCode { get; set; }

        [Required]
        [Column("div_name")]
        [StringLength(250)]
        [Display(Name = "Division Name")]
        public string DivisionName { get; set; }

        [Column("div_name_bangla")]
        [StringLength(250)]
        [Display(Name = "Division Name (Bangla)")]
        public string DivisionNameBangla { get; set; }

        [Column("sorting_order", Order = 2, TypeName = "int")]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Order")]
        public int? SortingOrder { get; set; }

    }

    public static class DbContextExtensions
    {
        public static IQueryable<object> Set(this DbContext context, Type t)
        {
            return (IQueryable<object>)context.GetType().GetMethod("Set")?.MakeGenericMethod(t).Invoke(context, null);
        }

        public static IQueryable<object> Set(this DbContext context, string table)
        {
            //One way to get the Type
            var tableType = context.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);

            //The Second way, get from the dictionary which we've initialized at startup
            //tableType = TableTypeDictionary[table];

            //The third way, works only if 'table' is an 'assembly qualified type name'
            tableType = Type.GetType(table);

            var objectContext = context.Set(tableType);
            return objectContext;
        }
    }
}
