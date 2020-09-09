using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class VehicleViewModel : Vehicle
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}
