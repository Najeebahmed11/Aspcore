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
