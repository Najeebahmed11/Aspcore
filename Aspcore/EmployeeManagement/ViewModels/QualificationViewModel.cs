using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class QualificationViewModel : Qualification
    {
        public Qualification Qualification { get; set; }
        public Guid Uid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
