using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UsersQualifications
    {
        [Key]
        public Guid Uid { get; set; }
      //  public Qualification Qualification { get; set; }
        public Guid UserId { get; set; }
        public UserQualfClaim UserQualfClaim { get; set; }

    }
}
