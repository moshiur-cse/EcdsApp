using EcdsApp.Models;
using EcdsApp.Models.DistrictWiseInfoModels;
using EcdsApp.Models.RegionModels;
using EcdsApp.Models.SystemCommon;
using EcdsApp.Models.TabularModels;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.UnionWiseInfoModels;
using EcdsApp.Models.UpazilaWiseInfoModels;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.UserMessage;
using EcdsApp.Models.ViewModels.TabularVm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace EcdsApp.Data
{
    //DbContext  //DataContext (JRCWebApp.Data)
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                //optionsBuilder.UseMySQL("server=server=130.180.3.146;userid=drip_admin;pwd=2022;database=ecds_db;Allow User Variables=True;");
                //optionsBuilder.UseMySQL("server=202.53.173.185;userid=rmo;pwd=RMO@2022;database=ecds_db;Allow User Variables=True;");
                optionsBuilder.UseMySQL("server=ims.cegisbd.com;userid=rmo;pwd=RMO@2022;database=ecds_db;Allow User Variables=True;SSL Mode=None");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public string GetConnectionString()
        {
            //return Database.GetDbConnection().ConnectionString = "server=202.53.173.179;userid=drip_admin;pwd=#UndP^drIp@2020;database=ecds_db;Allow User Variables=True;";
            //return Database.GetDbConnection().ConnectionString = "server=202.53.173.185;userid=rmo;pwd=RMO@2022;database=ecds_db;Allow User Variables=True;";
            return Database.GetDbConnection().ConnectionString = "server=ims.cegisbd.com;userid=rmo;pwd=RMO@2022;database=ecds_db;Allow User Variables=True;SSL Mode=None";
        }

        public DbSet<AdminBoundaryDistrict> AdminBoundaryDistricts { get; set; }
        public DbSet<AdminBoundaryDivision> AdminBoundaryDivisions { get; set; }
        public DbSet<AdminBoundaryUpazila> AdminBoundaryUpazilas { get; set; }
        public DbSet<AdminBoundaryUnion> AdminBoundaryUnions { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<SubTheme> SubThemes { get; set; }
        public DbSet<ThemeLayerDetail> ThemeLayerDetails { get; set; }
        public DbSet<ThemeLayerType> ThemeLayerTypes { get; set; }
        public DbSet<LayerLegendColor> LayerLegendColors { get; set; }
        public DbSet<LegendColorOption> LegendColorOptions { get; set; }
        public DbSet<MetaDataDetail> MetaDataDetails { get; set; }

        public DbSet<BundleDetail> BundleDetails { get; set; }

        public DbSet<TableInfo> TableInfos { get; set; }
        public DbSet<TableColumnInfo> TableColumnInfos { get; set; }
        public DbSet<ColumnType> ColumnTypes { get; set; }

        //Upazila Wise
        public DbSet<ExposureCategory> ExposureCategories { get; set; }
        public DbSet<UpazilaWiseExposureData> UpazilaWiseExposureData { get; set; }
        public DbSet<UpazilaWiseRiskIndex> UpazilaWiseRiskIndex { get; set; }
        public DbSet<UpazilaWisePoverty> UpazilaWisePoverties { get; set; }
        public DbSet<UpazilaWisePopulationDensity> UpazilaWisePopulationDensities { get; set; }




        public DbSet<DistrictWisePoverty> DistrictWisePoverties { get; set; }
        public DbSet<DistrictWisePopulation> DistrictWisePopulations { get; set; }
        public DbSet<DistrictWiseLightening> DistrictWiseLightenings { get; set; }
        public DbSet<DistrictWiseThunderstrom> DistrictWiseThunderstroms { get; set; }
        public DbSet<DistrictWisePopulationDensity> DistrictWisePopulationDensities { get; set; }

        //Union Wise
        public DbSet<FutureProjectionRainfall4Point5> FutureProjectionRainfall4Point5s { get; set; }
        public DbSet<FutureProjectionRainfall8Point5> FutureProjectionRainfall8Point5s { get; set; }

        public DbSet<FutureProjectionTemperatureMax4Point5> FutureProjectionTemperatureMax4Point5s { get; set; }
        public DbSet<FutureProjectionTemperatureMax8Point5> FutureProjectionTemperatureMax8Point5s { get; set; }

        public DbSet<FutureProjectionTemperatureMin4Point5> FutureProjectionTemperatureMin4Point5s { get; set; }
        public DbSet<FutureProjectionTemperatureMin8Point5> FutureProjectionTemperatureMin8Point5s { get; set; }

        public DbSet<FutureProjectionTemperatureMean4Point5> FutureProjectionTemperatureMean4Point5s { get; set; }
        public DbSet<FutureProjectionTemperatureMean8Point5> FutureProjectionTemperatureMean8Point5s { get; set; }

        public DbSet<BoundaryInfo> BoundaryInfos { get; set; }

        public DbSet<DataVerificationState> DataVerificationStates { get; set; }

        //public DbSet<UserAccessModule> UserAccessModules { get; set; }
        public DbSet<UserPermittedContent> UserPermittedContents { get; set; }
        public DbSet<RoleWisePermittedContent> RoleWisePermittedContents { get; set; }
        public DbSet<RoleWiseComponent> RoleWiseComponents { get; set; }

        //===add user message models
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReply> MessageReplies { get; set; }
        public DbSet<Status> Statuses { get; set; }

        //====Email Configuration and Chat Models

        public DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("users").Property(p => p.Id).HasColumnName("user_id");

            modelBuilder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnName("user_name");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.NormalizedUserName).HasColumnName("normalized_user_name");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.NormalizedEmail).HasColumnName("normalized_email");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.EmailConfirmed).HasColumnName("email_confirmed");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.SecurityStamp).HasColumnName("security_stamp");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PhoneNumber).HasColumnName("phone_number");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.LockoutEnd).HasColumnName("lockout_end");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.LockoutEnabled).HasColumnName("lockout_enabled");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.AccessFailedCount).HasColumnName("access_failed_count");

            modelBuilder.Entity<IdentityRole>().ToTable("user_role_lists").Property(p => p.Id).HasColumnName("role_id");
            modelBuilder.Entity<IdentityRole>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<IdentityRole>().Property(p => p.NormalizedName).HasColumnName("normalized_name");
            modelBuilder.Entity<IdentityRole>().Property(p => p.ConcurrencyStamp).HasColumnName("concurrency_stamp");

            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("user_roles").Property(p => p.RoleId).HasColumnName("role_id"); });
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

        public JsonResult GetTabularData(string columnName, string tableName)
        {

            try
            {
                var columnNameSepArray = columnName.Split(',');
                var sqlQry = "SELECT " + columnNameSepArray[0] + " AS Code, " + columnNameSepArray[1] + " AS Value FROM " + tableName + " ";
                var queryResult = ExecSql<JsonDataBindingViewModel>(sqlQry);
                //var jsonData = JsonSerializer.Serialize(queryResult);

                return new JsonResult(queryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return null;
        }

        public JsonResult GetAllData(string columnName, string tableName)
        {
            try
            {
                var sqlQry = "SELECT " + columnName + " FROM " + tableName + " ";
                var queryResult = ExecSql<JsonDataBindingViewModel>(sqlQry);
                //var jsonData = JsonSerializer.Serialize(queryResult);

                return new JsonResult(queryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return null;
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

        public async Task<DataTable> GetAllData(IList<string> tableColumn, string columnName, string tableName)
        {
            try
            {
                // Create a new DataTable.
                DataTable table = new DataTable();
                DataColumn column;
                DataRow row;

                foreach (var colName in tableColumn)
                {
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = colName;
                    column.AutoIncrement = false;
                    column.Caption = colName;
                    column.ReadOnly = false;
                    column.Unique = false;
                    // Add the column to the DataColumnCollection.
                    table.Columns.Add(column);
                }
                var fields = new List<string>();

                var conn = new MySqlConnection(GetConnectionString());
                await conn.OpenAsync();
                var cmd = new MySqlCommand("Select " + columnName + " from " + tableName + " ", conn);

                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    row = table.NewRow();
                    foreach (var colName in tableColumn)
                    {
                        var field = reader[colName].ToString();
                        if (!string.IsNullOrEmpty(field))
                        {
                            row[colName] = field;
                        }

                    }
                    table.Rows.Add(row);
                }

                await conn.CloseAsync();
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return new DataTable();
        }
    }

}
