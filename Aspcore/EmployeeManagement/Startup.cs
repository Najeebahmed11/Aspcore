using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddControllers(); services.AddControllers();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>(); //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
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
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseMvcWithDefaultRoute();
            //app.UseAuthorization();
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