using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class License
    {
        [Key]
        public Guid Category { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Alias { get; set; }
        public bool IsObselete { get; set; } //- Default value false
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<Guid> ModifiedByGuid { get; set; }
    }
}
