using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string MaxPassenger { get; set; }
        public Vehicle Vehicles { get; set; }

    }
}
