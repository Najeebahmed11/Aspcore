using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EmployeeManagement.Controllers
{
    public class QualificationController : Controller
    {
        private readonly AppDbContext context;
        private readonly IQualificationRepository qualificationRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public QualificationController(AppDbContext context,
            IQualificationRepository qualificationRepository, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.qualificationRepository = qualificationRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> DetailsQualification()
        {
            return View(await context.Qualifications.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteQualification(Guid uid)
        {
            Qualification user = context.Qualifications.FirstOrDefault(c => c.Uid == uid);

            context.Qualifications.Remove(user);
            await context.SaveChangesAsync();

            return View(await context.Qualifications.ToListAsync());

        }


        [HttpGet]
        public ViewResult EditQualifications(Guid uid)
        {
            Qualification qualification = context.Qualifications.FirstOrDefault(c => c.Uid == uid);
            //check if course is null than return back to view
            if (qualification == null)
            {
                return View();
            }
            QualificationViewModel qualificationViewModel = new QualificationViewModel
            {
                Name = qualification.Name,
                Description = qualification.Description
            };
            return View(qualificationViewModel);
        }

        [HttpPost]
        ////[Authorize]
        public IActionResult EditQualifications(QualificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Qualification qualifications = qualificationRepository.GetQualification(model.Uid);
                qualifications.Name = model.Name;
                qualifications.Description = model.Description;
                qualificationRepository.Update(qualifications);
                return RedirectToAction("ListQualifications");
             }
                    return View();
        }

                [HttpPost]
                [AllowAnonymous]
                public async Task<IActionResult> CreateQualificationAsync(QualificationViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                string uniqueFileName = null;
                //  return RedirectToAction("details", new { id = newEmployee.Id });	                if (model.Photo != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var qualification = new Qualification
                        {
                            Name = model.Name,
                            Description = model.Description,
                            Alias = model.Alias,
                            Code = model.Code,
                            Uid = Guid.NewGuid(),
                            // CreatedByGuid ..>extract from usermanager
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            //ModifiedBy = ..>Currrent user
                            IsDeleted = false,
                            IsObselete = false
                        };
                        context.Qualifications.Add(qualification);
                        await context.SaveChangesAsync();
                        return RedirectToAction("ListQualifications");
                    }
                    return View(model);
                }


                [HttpGet]
                public IActionResult CreateQualification()
                {

                    return View();
                }
                public async Task<IActionResult> ListQualifications()
                {
                    return View(await context.Qualifications.ToListAsync());
                }
                public IActionResult Index()
                {
                    return View();
                }
        public async Task<IActionResult> indexof()
        {
            return View(await context.Qualifications.ToListAsync());
        }
    }
        }
