using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UserQualfClaim
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QualificationId { get; set; }
        public bool IsSelected { get; set; }
        public string UserName { get; set; }
        public bool Verified { get; set; }
        public bool IsDisabled { get; set; }
        public Guid DisabledBy { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid DeleteBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public List<UsersQualifications> UsersQualfications { get; set; }
    }
}
