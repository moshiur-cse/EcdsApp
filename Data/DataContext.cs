using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Models;
using EcdsApp.Models.ThemeModels;
using System.Linq;
using EcdsApp.Models.TabularModels;
using EcdsApp.Models.UpazilaWiseInfoModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace EcdsApp.Data
{
    //DbContext  //DataContext (JRCWebApp.Data)
    public class DataContext : IdentityDbContext<UserRegistration, IdentityRole, string>
    {
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySQL("server=202.53.173.179;userid=drip_admin;pwd=#UndP^drIp@2020;database=ecds_db;Allow User Variables=True;");
        }

        public DbSet<AdminBoundaryDistrict> AdminBoundaryDistricts { get; set; }
        public DbSet<AdminBoundaryDivision> AdminBoundaryDivisions { get; set; }
        public DbSet<AdminBoundaryUpazila> AdminBoundaryUpazilas { get; set; }

        public virtual DbSet<UserRegistration> UserRegistration { get; set; }

        public DbSet<Theme> Themes { get; set; }
        public DbSet<SubTheme> SubThemes { get; set; }
        public DbSet<ThemeLayerDetail> ThemeLayerDetails { get; set; }
        public DbSet<ThemeLayerType> ThemeLayerTypes { get; set; }
        public DbSet<LayerLegendColor> LayerLegendColors { get; set; }
        public DbSet<MetaDataDetail> MetaDataDetails { get; set; }

        public DbSet<TableInfo> TableInfos { get; set; }
        public DbSet<TableColumnInfo> TableColumnInfos { get; set; }

        public DbSet<ExposureCategory> ExposureCategories { get; set; }
        public DbSet<UpazilaWiseExposureData> UpazilaWiseExposureData { get; set; }
        public DbSet<BoundaryInfo> BoundaryInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRegistration>().ToTable("users").Property(p => p.Id).HasColumnName("user_id");

            modelBuilder.Entity<UserRegistration>().Property(p => p.UserName).HasColumnName("user_name");
            modelBuilder.Entity<UserRegistration>().Property(p => p.NormalizedUserName).HasColumnName("normalized_user_name");
            modelBuilder.Entity<UserRegistration>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<UserRegistration>().Property(p => p.NormalizedEmail).HasColumnName("normalized_email");
            modelBuilder.Entity<UserRegistration>().Property(p => p.EmailConfirmed).HasColumnName("email_confirmed");
            modelBuilder.Entity<UserRegistration>().Property(p => p.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<UserRegistration>().Property(p => p.SecurityStamp).HasColumnName("security_stamp");
            modelBuilder.Entity<UserRegistration>().Property(p => p.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            modelBuilder.Entity<UserRegistration>().Property(p => p.PhoneNumber).HasColumnName("phone_number");
            modelBuilder.Entity<UserRegistration>().Property(p => p.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            modelBuilder.Entity<UserRegistration>().Property(p => p.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            modelBuilder.Entity<UserRegistration>().Property(p => p.LockoutEnd).HasColumnName("lockout_end");
            modelBuilder.Entity<UserRegistration>().Property(p => p.LockoutEnabled).HasColumnName("lockout_enabled");
            modelBuilder.Entity<UserRegistration>().Property(p => p.AccessFailedCount).HasColumnName("access_failed_count"); 
                                  
            modelBuilder.Entity<IdentityRole>().ToTable("user_role_lists").Property(p => p.Id).HasColumnName("role_id");          
            modelBuilder.Entity<IdentityRole>().Property(p => p.Name).HasColumnName("name");           
            modelBuilder.Entity<IdentityRole>().Property(p => p.NormalizedName).HasColumnName("normalized_name");           
            modelBuilder.Entity<IdentityRole>().Property(p => p.ConcurrencyStamp).HasColumnName("concurrency_stamp");

            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("user_roles").Property(p=>p.RoleId).HasColumnName("role_id"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.Property(p => p.UserId).HasColumnName("user_id"); });


            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("user_claims").Property(p => p.Id).HasColumnName("id"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.Property(p => p.UserId).HasColumnName("user_id"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.Property(p => p.ClaimType).HasColumnName("claim_type"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.Property(p => p.ClaimValue).HasColumnName("claim_value"); });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("user_role_claims").Property(p => p.Id).HasColumnName("id"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.Property(p => p.RoleId).HasColumnName("role_id"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.Property(p => p.ClaimType).HasColumnName("claim_yype"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.Property(p => p.ClaimValue).HasColumnName("claim_value"); });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("user_logins").Property(p => p.UserId).HasColumnName("user_id"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.Property(p => p.LoginProvider).HasColumnName("login_provider"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.Property(p => p.ProviderKey).HasColumnName("provider_key"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.Property(p => p.ProviderDisplayName).HasColumnName("provider_display_name"); });

            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("user_tokens").Property(p => p.UserId).HasColumnName("user_id"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.Property(p => p.Name).HasColumnName("name"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.Property(p => p.Value).HasColumnName("value"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.Property(p => p.LoginProvider).HasColumnName("login_provider"); });

        }


        // Reference:  https://stackoverflow.com/questions/48278258/entity-framework-core-raw-sqlqueries-with-custom-model
        public List<T> ExecSql<T>(string query)
        {
            using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            Database.OpenConnection();

            var list = new List<T>();
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    var obj = Activator.CreateInstance<T>();
                    foreach (var prop in obj.GetType().GetProperties())
                    {                            
                        if (!Equals(result[prop.Name], DBNull.Value))
                        {                                
                            prop.SetValue(obj, result[prop.Name], null);
                        }                                                                                   
                    }
                    list.Add(obj);
                }
            }
            Database.CloseConnection();
            return list;
        }

        //#endregion

        //public DbSet<JRCWebApp.ViewModels.Role> Role { get; set; }
        public List<string> GetColumns<TEntity>(string modelName)
        {
            var property = typeof(TEntity)
                .GetProperties()
                .Single(s => s.Name == modelName);
            

            return property.PropertyType
                .GetGenericArguments() //Get the generic type of the DbSet
                .SelectMany(t => t.GetProperties()
                    .Select(pi => pi.Name)).ToList();


        }
        public static List<string> GetColumn(string modelName)
        {
            var property = typeof(DbContext)
                .GetProperties()
                .Single(s => s.Name == modelName);

            return property.PropertyType
                .GetGenericArguments() //Get the generic type of the DbSet
                .SelectMany(t => t.GetProperties()
                    .Select(pi => pi.Name)).ToList();
        }


    }


    public static class CustomExtensions
    {
        public static IQueryable Query(this DbContext context, string entityName) => context.Query(context.Model.FindEntityType(entityName).ClrType);

        public static IQueryable Query(this DbContext context, Type entityType) =>
            (IQueryable)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType);
    }   
}
