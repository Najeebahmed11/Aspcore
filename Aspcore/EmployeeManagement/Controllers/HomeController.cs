using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    //  [Route("~/Home")]
    public class HomeController : Controller
    {
        
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
      //  [Route("")]
       // [Route("~/")]
        public ViewResult Index()
        {
            var model= _employeeRepository.GetAllEmployee();
            return View("~/Views/Home/Index.cshtml",model);
        }
        //[Route("~/{id?}")]
        public ViewResult Details(int? id,string name)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "EmployeeDetails"
            };
            //in relative path we do not use extension
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
            }
    }
}
