using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CourseViewModel
    {
        public Guid Uid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
      
    }
}
