using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class LicenseViewModel : License
    {
        public Guid Category { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
