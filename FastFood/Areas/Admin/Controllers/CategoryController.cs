using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Repository.IRepository;
using FastFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
           var categories = _unitOfWork.Category.GetAll().Select(x => new CategoryVM()
           {
               Id = x.Id,
               Title = x.Title
           });
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM();
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (ModelState.IsValid) { 
            Category category = new Category()
            {
               Id = categoryVM.Id,
               Title = categoryVM.Title
            };
        
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null){
                return NotFound();
            }
            var category = _unitOfWork.Category.Get(x => x.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            CategoryVM categoryVM = new CategoryVM()
            {
               Title = category.Title,
               Id = category.Id
            };
            return View(categoryVM);

        }
        [HttpPost]

        public IActionResult Edit(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
            var category = _unitOfWork.Category.Get(x => x.Id == categoryVM.Id);
                if (category != null)
                {
                    _unitOfWork.Category.Update(categoryVM);
                    _unitOfWork.Save();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryVM);

        }


        #region Api Calls
        [HttpDelete]
  
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objToBeDeleted = _unitOfWork.Category.Get(u => u.Id == id);
            if (objToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(objToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }

}
