using EmployeeManagement.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
                {
                    new Employee(){Id=1,Name="najeeb",Department="cs"},
                    new Employee() { Id = 2, Name = "Hinan", Department = "cs" }
                };
        }
        public Employee GetEmployee(int Id)
        {
            throw new NotImplementedException();
        }
    }

}
