using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 1,
                        Name = "Najeeb Ahmed",
                        Department = Dept.IT,
                        Email = "najeeeeb2017@maal.com"
                    },
                    new Employee
                    {
                        Id = 2,
                        Name = "Najeeb ",
                        Department = Dept.HR,
                        Email = "najeeeeb27@maal.com"
                    }

                );
        }
    }
}
