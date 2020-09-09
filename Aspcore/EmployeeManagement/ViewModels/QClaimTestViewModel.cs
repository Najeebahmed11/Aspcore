using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class QClaimTestViewModel
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid QualificationId { get; set; }
        public bool Verified { get; set; }
        public IEnumerable<Qualification> Qualifications { get; set; }
    }
}
