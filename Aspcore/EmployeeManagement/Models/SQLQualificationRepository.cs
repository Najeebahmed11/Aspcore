using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLQualificationRepository : IQualificationRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLQualificationRepository> logger;

        public SQLQualificationRepository(AppDbContext context
            , ILogger<SQLQualificationRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Qualification GetQualification(Guid uid)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");
            return context.Qualifications.Find(uid);
        }
        public Qualification Update(Qualification qualificationchanges)
        {
            var qualification = context.Qualifications.Attach(qualificationchanges);
            qualification.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return qualificationchanges;
        }
    }
}
