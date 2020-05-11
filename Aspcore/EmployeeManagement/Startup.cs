using Aspose.Email.Clients.Graph;
using EmployeeManagement.Models;
using EmployeeManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
//using System.Web.Http;


namespace EmployeeManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;


        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            //services.AddRazorPages();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

            }).AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders()
             .AddTokenProvider<CustomEmailConfirmationTokenProvider
             <ApplicationUser>>("CustomEmailConfirmation");



            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(5));

            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromDays(3));

            services.AddMvc(options=> {
                var policy = new AuthorizationPolicyBuilder()
                                                        .RequireAuthenticatedUser()
                                                        .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();

            services.AddAuthentication()
                .AddGoogle(Options =>
                {
                    Options.ClientId = "438200310044-qtmmdm89m76ljghdlrr21v0uqseouc5h.apps.googleusercontent.com";
                    Options.ClientSecret = "JAMX4BeIgEj6FW6DQrzLM4_y";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "563796630936584";
                    options.AppSecret = "924e4ef25febea78225f4e8ea1af4278";
                });


            services.ConfigureApplicationCookie(options=> 
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });



            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));


                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));




                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin")

                    ); 
            });

            services.AddControllers(); services.AddControllers();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddSingleton<IAuthorizationHandler,CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
        }
        //kestro in itself webserver
        //it can use incoming http req
        //by using dot net exe
        //kestrol can be used with reverse proxy server
        //
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
             //   app.UseExceptionHandler("/Error");
              //  app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            //app.UseMvcWithDefaultRoute();
            app.UseAuthorization();
            //we can not use mvc routes now now we have to use end 
              app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
               // endpoints.MapControllers();
            });
        }
        //33
        

    }
}