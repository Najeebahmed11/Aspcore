using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class CompanyController : Controller
    {
        private readonly AppDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        public CompanyController(AppDbContext context, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Create( Company uQ)
        {
            if (ModelState.IsValid)
            {
                //if (uQ. == null )
                //{
                //    return View(uQ);
                //}

                //if (UserHasAlreadyCurrentQualificationClaim(uQ.UserId, uQ.QualificationId))
                //{
                //    return RedirectToAction(nameof(Index));
                //}

                uQ.VehicleGuid = Guid.NewGuid();
                uQ.CompanyGuid = Guid.NewGuid();


                context.Add(uQ);
                await context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(uQ);
        }

        public async Task<IActionResult> Create()
        {
            var userID = (await userManager.GetUserAsync(HttpContext.User)).Id;

            var vehicle = await context.Vehicles.ToListAsync();
            // Users registered qualification Claims

            var viewModel = new QClaimVehicleViewModel
            {
                UserID = Guid.Parse(userID),
                Vehicles = vehicle
            };


            return View("Create", viewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
