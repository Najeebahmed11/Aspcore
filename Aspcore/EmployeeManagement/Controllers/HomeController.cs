using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController()
        {
            _employeeRepository = new MockEmployeeRepository();
        }
        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }
        public ViewResult Details()
        {
            //in relative path we do not use extension
            Employee model = _employeeRepository.GetEmployee(1);
            ViewData["Employee"] = model;
            ViewData["PageTittle"] = "EmployeeDetails";
            return View();
        }

    }
}
