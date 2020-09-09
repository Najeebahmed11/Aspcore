using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CreateUserQualificationClaimViewModel
    {
        [Key]
        public Guid UserId { get; set; }
        public Guid QualificationId { get; set; }
        public bool Verified { get; set; }
        public virtual IEnumerable<Qualification> Qualification { get; set; }
    }
}
