using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult index()
        {
            return Json(new { id = 1, name = "najeeb" }); 
        }
    }
}
