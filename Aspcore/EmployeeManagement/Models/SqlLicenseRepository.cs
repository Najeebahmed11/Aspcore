using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlLicenseRepository : ILicenseRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SqlLicenseRepository> logger;

        public SqlLicenseRepository(AppDbContext context
            , ILogger<SqlLicenseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public License Update(License licensechanges)
        {
            var license = context.Licenses.Attach(licensechanges);
            license.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return licensechanges;
        }
        public License GetLicense(Guid category)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");
            return context.Licenses.Find(category);
        }
    }
}
