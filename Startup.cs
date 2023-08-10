using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;


namespace EcdsApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDatabaseDeveloperPageExceptionFilter();
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddControllersWithViews().AddSessionStateTempDataProvider();
            //services.AddRazorPages().AddSessionStateTempDataProvider();
            //services.AddMvc();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache(); //RMO
            //services.AddDbContext<DataContext>();

            services.AddDbContext<DataContext>(ServiceLifetime.Transient);

            _ = services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_@";
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;

                //adding lockout user options

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 3;


                // options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            }).AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders().AddDefaultUI();


            //====External Login Provider

            services.AddAuthentication().AddCookie()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                //options.ClientId = "444141584048-ecgn1sa3ubmvrbmmd7ocpl8sehvu3vpm.apps.googleusercontent.com";
                //options.ClientSecret = "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";
                options.ClientId = "636285168617-vgnq3f6h1cvc8slmu6vhict38nvdael8.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.Scope.Add("profile");
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });


            services.Configure<FormOptions>(x => x.ValueCountLimit = 104857600);

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = ".ECDS-APP.Session";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(25);
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            //Add application services.
            //services.AddTransient<IEmailSender, EmailSender>();
            //services.AddSingleton<IEmailSender, IEmailSender>();
            //services.AddTransient<IEmailSender, IEmailSender>(i =>
            //    new EmailSender(
            //        Configuration["EmailSender:Host"],
            //        Configuration.GetValue<int>("EmailSender:Port"),
            //        Configuration.GetValue<bool>("EmailSender:EnableSSL"),
            //        Configuration["EmailSender:UserName"],
            //        Configuration["EmailSender:Password"]
            //    )
            //);

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
                options.SortExpressionParameterName = "sort";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddRazorPages().AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                //options.Cookie.HttpOnly = true;
                //options.Cookie.Name = ".PDB-APP.Session";
                //options.IdleTimeout = TimeSpan.FromMinutes(20);
                //options.IOTimeout = TimeSpan.FromMinutes(20);



                //Add By RMO
                // Set a short timeout for easy testing.
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.AddMvc()
                .AddSessionStateTempDataProvider()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseForwardedHeaders(); //For Ip Get
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Dashboard}/{id?}");
                endpoints.MapRazorPages();

                //endpoints.MapControllerRoute(
                //    name: "signin-url",
                //    pattern: "{controller=Home}/{action=GoogleResponse}");
                //endpoints.MapRazorPages();

            });

        }
    }
}