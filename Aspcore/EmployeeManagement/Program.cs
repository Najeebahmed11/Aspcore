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
           //configuring the req
           //run midl can only write the message
           //terminal middleware:that do not call next middleware
           //we have to (use) and (next) comand for this
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
