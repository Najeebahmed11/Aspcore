using Aspose.Email.Tools.Logging;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    //  [Route("~/Home")]
    public class HomeController : Controller
    {
        
        private readonly IEmployeeRepository _employeeRepository;
      //  [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository,
            IHostingEnvironment hostingEnvironment,ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }
      //  [Route("")]
       // [Route("~/")]
       [AllowAnonymous]
        public ViewResult Index()
        {
            var model= _employeeRepository.GetAllEmployee();
            return View(model);
        }
        //[Route("~/{id?}")]
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug log");
            logger.LogInformation("Information log");
            logger.LogWarning("Warning log");
            logger.LogError("Error log");
            logger.LogCritical("Critical log");

            // throw new Exception("Errors in deatils view");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id.Value);

            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,        
                PageTitle = "EmployeeDetails"
            };
            //in relative path we do not use extension
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        //[Authorize]
        public ViewResult Create()
        {
            return View();
        }
        [HttpGet]
        //[Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExsistingPhotoPath = employee.Photopath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExsistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExsistingPhotoPath);
                            System.IO.File.Delete(filePath);
                    }
                    employee.Photopath = ProcessUploadedFiles(model);
                }
                
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFiles(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFiles(model);

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
