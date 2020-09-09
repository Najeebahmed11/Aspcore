using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class UserQualfClaimViewModel
    {
        public string UserId { get; set; }
        public string QualificationId { get; set; }
        public bool IsSelected { get; set; }
        public string UserName { get; set; }
        //construct list of UserQualificationsClaims
    }
}
