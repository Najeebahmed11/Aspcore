using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface ICourseRepository
    {
        Course GetCourse(Guid Uid);
        IEnumerable<Course> GetAllCourse();
        Course Update(Course coursechanges);
        Course Delete(Guid Uid);
    }
}
