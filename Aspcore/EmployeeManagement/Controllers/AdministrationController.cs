using Aspose.Email.Clients.Graph;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
//using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    //[Authorize(Policy ="AdminRolePolicy")]
    public class AdministrationController: Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdministrationController> logger;
        private readonly AppDbContext context;
        private readonly ICourseRepository courseRepository;
        private readonly ILicenseRepository licenseRepository;
        private readonly IQualificationRepository qualificationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public object ViewData { get; private set; }

        public AdministrationController(RoleManager<IdentityRole> roleManager
                                        ,UserManager<ApplicationUser> userManager
                                        ,ILogger<AdministrationController> logger, AppDbContext context
                              ,ICourseRepository courseRepository, ILicenseRepository licenseRepository
                              , IQualificationRepository qualificationRepository, IHttpContextAccessor httpContextAccessor)
             
        {
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
            this.userManager = userManager; 
            this.logger = logger;
            this.context = context;
            this.courseRepository = courseRepository;
            this.licenseRepository = licenseRepository;
            this.qualificationRepository = qualificationRepository;
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            //License user = context.Licenses.FirstOrDefault(c => c.Category == category);

            //context.Licenses.Remove(user);
            //await context.SaveChangesAsync();
            UserQualfClaim userQualificationClaim =  context.UserQualfClaims.FirstOrDefault(c=>c.Id==id);
            context.UserQualfClaims.Remove(userQualificationClaim);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userQualificationClaim = await context.UserQualfClaims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userQualificationClaim == null)
            {
                return NotFound();
            }

            return View(userQualificationClaim);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var userQualificationClaim = await _context.UserQualificationClaims
            //    .FirstOrDefaultAsync(m => m.UserID == id);
            var userQualificationClaim = await context.UserQualfClaims
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userQualificationClaim == null)
            {
                return NotFound();
            }

            return View(userQualificationClaim);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserID,QualificationId,Verified,IsDisabled,DisabledBy,ExpiryDate,IsDeleted,CreatedDate,CreatedByGuid,DeletedDate,DeletedByGuid,ModifiedDate,ModifiedBy")] UserQualfClaim userQualificationClaim)
        {
            if (id != userQualificationClaim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(userQualificationClaim);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userQualificationClaim);
        }




        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userQualificationClaim = await context.UserQualfClaims.FirstOrDefaultAsync(x => x.Id == id);// .FindAsync(id);
            if (userQualificationClaim == null)
            {
                return NotFound();
            }
            return View(userQualificationClaim);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,QualificationId,ExpiryDate")] UserQualfClaim uQ)
        {
            if (ModelState.IsValid)
            {
                if (uQ.UserId == null || uQ.QualificationId == null)
                {
                    return View(uQ);
                }

                //if (UserHasAlreadyCurrentQualificationClaim(uQ.UserId, uQ.QualificationId))
                //{
                //    return RedirectToAction(nameof(Index));
                //}

                uQ.Id = Guid.NewGuid();
                //uQ.UserID = ;
                uQ.Verified = false;
                uQ.IsDisabled = false;
                //userQualificationClaim.DisabledBy;                
                uQ.IsDeleted = false;
               
                //DeletedDate;
                //DeletedByGuid;
                //ModifiedDate;
                //ModifiedBy

                context.Add(uQ);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uQ);
        }


        public async Task<IActionResult> Create()
        {
            var userID = (await userManager.GetUserAsync(HttpContext.User)).Id;

            var qualifications = await context.Qualifications.ToListAsync();
            // Users registered qualification Claims

            var viewModel = new QClaimTestViewModel
            {
                UserID = Guid.Parse(userID),
                Qualifications = qualifications
            };


            return View("Create", viewModel);
        }


        //public async Task<IActionResult> Create()
        //{
        //    var user = await userManager.GetUserAsync(HttpContext.User);
        //    ViewBag.UserID = user.Id;
        //    var qualifications = await context.Qualifications.ToListAsync();

        //    var Test = qualifications.Select(x => new SelectListItem
        //    {
        //        Text = x.Name,
        //        Value = x.Uid.ToString()
        //    }).ToList();

        //    return View();
        //}


        public async Task<IActionResult> Index()
        {
            return View(await context.UserQualfClaims.ToListAsync());
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserQualfClaimAsync(UserQualfClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userQualfClaim = new UserQualfClaim
                {
                    UserName = model.UserName,
                    UserId = Guid.NewGuid(),
                    // CreatedByGuid ..>extract from usermanager
                    ModifiedDate = DateTime.Now,
                    //ModifiedBy = ..>Currrent user
                    IsDeleted = false
                };
                context.UserQualfClaims.Add(userQualfClaim);
                await context.SaveChangesAsync();
                return RedirectToAction("ListUserClaimQualifications");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateUserQualfClaim()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListUserClaimQualifications()
        {
            return View(await context.UserQualfClaims.ToListAsync());
        }




        //[HttpGet]
        //public async Task<IActionResult> EditUserQualificationClaims(Guid userId)
        //{

        //    ViewBag.userId = userId;

        //    Qualification user = context.Qualifications.FirstOrDefault(c => c.Uid == userId);

        //    if (user == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
        //        return View("NotFound");
        //    }

        //    var model = new List<UserQualfClaimViewModel>();

        //    foreach (var role in roleManager.Roles)
        //    {
        //        var userRolesViewModel = new UserRolesViewModel
        //        {
        //            RoleId = role.Id,
        //            RoleName = role.Name
        //        };

        //        if (await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            userRolesViewModel.IsSelected = true;
        //        }
        //        else
        //        {
        //            userRolesViewModel.IsSelected = false;
        //        }

        //        model.Add(userRolesViewModel);
        //    }

        //    return View(model);
        //}





        [HttpGet]
        public async Task<IActionResult> DetailsLicense()
        {
            return View(await context.Licenses.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLicense(Guid category)
        {
            License user = context.Licenses.FirstOrDefault(c => c.Category == category);

            context.Licenses.Remove(user);
            await context.SaveChangesAsync();

            return View(await context.Licenses.ToListAsync());

        }


        [HttpGet]
        public ViewResult EditLicenses(Guid category)
        {
            License license = context.Licenses.FirstOrDefault(c => c.Category == category);
            //check if course is null than return back to view
            if (license == null)
            {
                return View();
            }
            LicenseViewModel licenseViewModel = new LicenseViewModel
            {
                CategoryName = license.CategoryName,
            };
            return View(licenseViewModel);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult EditLicenses(LicenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                License license = licenseRepository.GetLicense(model.Category);
                license.CategoryName = model.CategoryName;
                licenseRepository.Update(license);
                return RedirectToAction("ListLicenses");
            }
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateLicenseAsync(LicenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var license = new License
                {
                    CategoryName = model.CategoryName,

                    Category = Guid.NewGuid()
                    // CreatedByGuid ..>extract from usermanager
                    
                };
                context.Licenses.Add(license);
                await context.SaveChangesAsync();
                return RedirectToAction("ListLicenses");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateLicense()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListCourses()
        {
            return View(await context.Courses.ToListAsync());
        }



        [HttpGet]
        public async Task<IActionResult> DetailsCourse()
        {
            return View(await context.Courses.ToListAsync());
        }

        [HttpGet]
        public ViewResult EditCourses(Guid uid)
        {
            Course course = context.Courses.FirstOrDefault(c => c.Uid == uid);
            //check if course is null than return back to view
            if (course == null)
            {
                return View();
            }
            CourseEditViewModel courseEditViewModel = new CourseEditViewModel
            {
                Name = course.Name,
                Description = course.Description
            };
            return View(courseEditViewModel);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult EditCourses(CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepository.GetCourse(model.Uid);
                course.Name = model.Name;
                course.Description = model.Description;
                courseRepository.Update(course);
                return RedirectToAction("ListCourses");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListLicenses()
        {
            return View(await context.Licenses.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> DeleteCourse(Guid uid)
        {
            Course user = context.Courses.FirstOrDefault(c => c.Uid == uid);

            context.Courses.Remove(user);
            await context.SaveChangesAsync();

            return View(await context.Courses.ToListAsync());

        }


        [HttpPost]
        public async Task<IActionResult> DeleteCourses(Guid uid)
        {
                Course user =  context.Courses.FirstOrDefault(c => c.Uid == uid);
            
                context.Courses.Remove(user);
                await context.SaveChangesAsync();
                
                return View(await context.Courses.ToListAsync());
            
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCourseAsync(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
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
                context.Courses.Add(course);
                await context.SaveChangesAsync();
                return RedirectToAction("ListCourses");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = userId
            };

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value=="true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }

            // Get all the user existing claims and delete them
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }

            // Add all the claims that are selected on the UI
            result = await userManager.AddClaimsAsync(user,
                model.Cliams.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true":"false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });

        }


        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }


        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError($"Error deleting role");
                    //Log the exception to a file. We discussed logging to a file
                    // logger.LogError($"Exception Occured : {ex}");
                    // Pass the ErrorTitle and ErrorMessage that you want to show to
                    // the user using ViewBag. The Error view retrieves this data
                    // from the ViewBag and displays to the user.
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. If you want to delete this role, please remove the users from the role and then try to delete";
                    return View("Error");
                }
            }
        }


        [HttpGet]
        public IActionResult ListUsers()
        {
            var users= userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                City = user.City,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            // GetClaimsAsync retunrs the list of user Claims
            
            //return View(model);
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name=model.RoleName

                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
      //  [Authorize(Policy="EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }


        [HttpPost]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result=await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user=await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }


            return RedirectToAction("EditRole", new { Id = roleId });
        }

    }
}
