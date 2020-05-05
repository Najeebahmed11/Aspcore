using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//C:\Users\Najeeb Tiwana\Source\repos\Aspcore\Aspcore\EmployeeManagement\appsettings.json
namespace EmployeeManagement
{
    public class Program
    {
        // 2020-05-05
        // Connection String was wrong corrected in appsettings.json
        // EntityFrameWork removed and added EntityFrameWorkCore Instead
        // Migrations folder deleted.
        // Removed: using System.Data.Entity from AppDbContext.
        // Run: "Add-Migration Init" and "update-database" in Package Manager Console 
        // Now database is ready to be used.

        public static void Main(string[] args)
        {
           //lesson 15
           //mvc:it is user iterface layer
           //model consisit of data
           //controller:that handles the request and respond
           //
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
