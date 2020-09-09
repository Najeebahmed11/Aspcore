using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CourseEditViewModel : Course
    {
        public Guid Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
