using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Course
    {
        [Key]
        public Guid Uid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsObselete { get; set; } //- Default value false
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedByGuid { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        public Nullable<Guid> DeletedByGuid { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<Guid> ModifiedByGuid { get; set; }
    }
}
