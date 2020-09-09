using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class QClaimVehicleViewModel
    {
        [Key]
        public Guid UserID { get; set; }
        public Guid Id { get; set; }
        public bool Verified { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
