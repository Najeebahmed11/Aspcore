using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class VehicleController : Controller
    {
        private readonly AppDbContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        public VehicleController(AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(Guid uid)
        {
            //License user = context.Licenses.FirstOrDefault(c => c.Category == category);

            //context.Licenses.Remove(user);
            //await context.SaveChangesAsync();
            Vehicle vehicle = context.Vehicles.FirstOrDefault(c => c.Id == uid);
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
            return RedirectToAction("ListVehicles");
        }
        public async Task<IActionResult> DetailsVehicle(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var userQualificationClaim = await _context.UserQualificationClaims
            //    .FirstOrDefaultAsync(m => m.UserID == id);
            var vehicle = await context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        [HttpPost]
        public async Task<IActionResult> EditVehicles(Guid id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(vehicle);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
                return RedirectToAction("ListVehicles");
            }
            return View(vehicle);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteVehicle(Guid? uid)
        {
            if (uid == null)
            {
                return NotFound();
            }

            var vehicle = await context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == uid);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }




        [HttpGet]
        public ViewResult EditVehicles(Guid uid)
        {
            Vehicle vehicle = context.Vehicles.FirstOrDefault(c => c.Id == uid);
            //check if course is null than return back to view
            if (vehicle == null)
            {
                return View();
            }
            VehicleViewModel vehicleViewModel = new VehicleViewModel
            {
                Brand = vehicle.Brand,
                Category = vehicle.Category,
                Id=vehicle.Id
            };
            return View(vehicleViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateVehicleAsync(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = null;
                ////  return RedirectToAction("details", new { id = newEmployee.Id });	                if (model.Photo != null)
                //{
                //    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                //    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //}
                var vehicle = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    Category=model.Category,
                    Brand=model.Brand
                    // CreatedByGuid ..>extract from usermanager
                };
                context.Vehicles.Add(vehicle);
                await context.SaveChangesAsync();
                return RedirectToAction("ListVehicles");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateVehicle()
        {

            return View();
        }
        public async Task<IActionResult> ListVehicles()
        {
            return View(await context.Vehicles.ToListAsync());
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
