using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Data;
using FastFood.Repository.Repository.IRepository;
using FastFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FastFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManagment(string userId)
        {
            var userFromDb = _unitOfWork.ApplicationUser.Get(u =>u.Id == userId);
            RoleManagmentVM roleVM = new()
            {
                ApplicationUser = userFromDb,
                RoleList = _roleManager.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                })
            };
            roleVM.ApplicationUser.Role = _userManager.GetRolesAsync(userFromDb)
                .GetAwaiter().GetResult().FirstOrDefault();
            return View(roleVM);
        }
        [HttpPost]
        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser
                .Get(u => u.Id == roleManagmentVM.ApplicationUser.Id);
           string oldRole = _userManager. GetRolesAsync(applicationUser).GetAwaiter().GetResult().FirstOrDefault();
            
            if(!(roleManagmentVM.ApplicationUser.Role == oldRole))
            {
             
                
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser,roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();
                TempData["success"] = "Operation Successful";

            }
            return RedirectToAction(nameof(Index));

        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll().ToList();
            foreach (ApplicationUser user in users)
            {
                //assign the role of the each user to the role property 
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            }
            return Json(new { data = users });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error, Operation failed" });
            }
            if(objFromDb.LockoutEnd != null && objFromDb.LockoutEnd> DateTime.UtcNow)
            {
                objFromDb.LockoutEnd = DateTime.UtcNow;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.UtcNow.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful" });
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var objToBeDeleted = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ApplicationUser.Remove(objToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion
    }
}
