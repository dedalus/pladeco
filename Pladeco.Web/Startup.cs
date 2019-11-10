using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pladeco.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pladeco.Web.Helpers;
using Pladeco.Model;
using Pladeco.Web.Data.Data;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace Pladeco.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<User, Role>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            opt =>
            {
                //configure your other properties
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/NotAuthorized";
            });

            services.AddTransient<SeedDb>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();

            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            // var webRootPath = env.WebRootPath;
            // call rotativa conf passing env to get web root path
            //RotativaConfiguration.Setup(env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Dashboard/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes
                .MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}")

                .MapRoute(
                    name: "default_plans",
                    template: "plans/{id?}", new { controller = "Plans", action = "Details" })

                .MapRoute(
                    name: "default_projects",
                    template: "projects/{id?}", new { controller = "Projects", action = "Details" })

                .MapRoute(
                    name: "default_plantask",
                    template: "plantasks/{id?}", new { controller = "PlanTasks", action = "Details" });
            });
        }
    }
}
