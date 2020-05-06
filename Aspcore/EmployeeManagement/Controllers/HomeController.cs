using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    //  [Route("~/Home")]
    public class HomeController : Controller
    {
        
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadFolder=Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName=Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath=Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email=model.Email,
                    Department=model.Department,
                    Photopath=uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
            }
    }
}
