using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLCourseRepository : ICourseRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLCourseRepository> logger;

        public SQLCourseRepository(AppDbContext context
            , ILogger<SQLCourseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Course Delete(Guid Uid)
        {
            Course course = context.Courses.Find(Uid);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            return course;

        }
        public Course Update(Course coursechanges)
        {
            var course = context.Courses.Attach(coursechanges);
            course.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return coursechanges;
        }

        public IEnumerable<Course> GetAllCourse()
        {
            return context.Courses;
        }

        public Course GetCourse(Guid uid)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");
            return context.Courses.Find(uid);
        }
    }
}
