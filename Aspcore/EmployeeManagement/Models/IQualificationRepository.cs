using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IQualificationRepository
    {
        Qualification GetQualification(Guid Uid);
        Qualification Update(Qualification qualificationchanges);
    }
}
