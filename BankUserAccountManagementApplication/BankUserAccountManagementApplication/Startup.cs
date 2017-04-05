using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BankUserAccountManagmentApplicationDAL.Data;
using BankUserAccountManagmentApplicationDAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BankUserAccountManagementApplication.AuthorizationPolicies.Requirements;
using BankUserAccountManagmentDAL.Enums;
using Microsoft.AspNetCore.Authorization;
using BankUserAccountManagementApplication.AuthorizationPolicies;

namespace BankUserAccountManagementApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<BankUserAccountManagementContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IBaseRepository, BaseRepository>();
            // Add framework services.
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                                  policy => policy.Requirements.Add(new AdminAuthorizationPolicyRequirement(RolesEnum.Admin.ToString())));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewEditResourceAsSelfOrAdmin",
                                  policy => policy.Requirements.Add(new UserProfileAuthorizationPolicyRequirement()));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BankAccountOperations",
                                  policy => policy.Requirements.Add(new BankAccountOperationsAuthorizationPolicyRequirement(RolesEnum.Admin.ToString())));
            });

            services.AddSingleton<IAuthorizationHandler, AdminRoleHandlerPolicy>();
            services.AddSingleton<IAuthorizationHandler, ViewEditResourceHandlerPolicy>();
            services.AddSingleton<IAuthorizationHandler, BankAccountOperationsHandlePolicy>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IBaseRepository baseRepository)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new PathString("/Account/Login/"),
                //AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ExpireTimeSpan = TimeSpan.FromHours(2),
                SlidingExpiration = true,  
            });

            app.UseClaimsTransformation(context =>
            {
                if (context.Principal.Identity.IsAuthenticated)
                {
                    context.Principal.Identities.First().AddClaim(new Claim("now", DateTime.Now.ToString()));
                }

                return Task.FromResult(context.Principal);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(baseRepository.Context);
        }
    }
}
