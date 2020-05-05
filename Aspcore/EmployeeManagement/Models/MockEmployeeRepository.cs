using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
                {
                    new Employee(){Id=1,Name="najeeb",Department=Dept.HR,Email="najeeb@gmail.cpm"},
                    new Employee() { Id = 2, Name = "Hinan", Department = Dept.IT,Email="hinan@gmail.cpm" }
                };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }

}
