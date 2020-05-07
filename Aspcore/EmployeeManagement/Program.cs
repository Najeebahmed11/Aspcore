using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using NLog.Extensions.Logging;
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
       WebHost.CreateDefaultBuilder(args)
       .ConfigureLogging((hostingContext, logging) =>
       {
           logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
           logging.AddConsole();
           logging.AddDebug();
           logging.AddEventSourceLogger();
            // Enable NLog as one of the Logging Provider
            logging.AddNLog();
       })
       .UseStartup<Startup>();
    }
}
