using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Repository.IRepository;
using FastFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FastFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class SubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categories = _unitOfWork.SubCategory.GetAll(includeProperties: "Category");
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SubCategoryVM subCategoryVM = new SubCategoryVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                })
            };

            return View(subCategoryVM);
        }
        [HttpPost]
        public IActionResult Create(SubCategoryVM subCategoryVM)
        {
            SubCategory subCategory = new SubCategory();
            if (ModelState.IsValid) {
                subCategory.Id = subCategoryVM.Id;
                subCategory.Title = subCategoryVM.Title;
                subCategory.CategoryId = subCategoryVM.CategoryId;
                _unitOfWork.SubCategory.Add(subCategory);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            subCategoryVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            });
                return View(subCategoryVM);
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            SubCategory subCategory = _unitOfWork.SubCategory
                .Get(u=> u.Id == id, includeProperties:"Category");

            if (subCategory == null)
            {
                return NotFound();
            }

            SubCategoryVM subCategoryVM = new SubCategoryVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }),
                Id = subCategory.Id,
                Title = subCategory.Title,
                CategoryId = subCategory.CategoryId,

            };

            ViewBag.Category = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Title");
            return View(subCategoryVM);
        }
        [HttpPost]
        public IActionResult Edit(SubCategoryVM subCategoryVM)
        {
            SubCategory subCategory = _unitOfWork.SubCategory.Get(u => u.Id == subCategoryVM.Id);
            if (ModelState.IsValid)
            {
                subCategory.Title = subCategoryVM.Title;
                subCategory.CategoryId = subCategoryVM.CategoryId;
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                subCategoryVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
                ViewBag.Category = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Title");
            return View(subCategory);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<SubCategory> subCategories = _unitOfWork.SubCategory.GetAll().ToList();
            return Json(new { data = subCategories });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objToBeDeleted = _unitOfWork.SubCategory.Get(u => u.Id == id);
            if (objToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SubCategory.Remove(objToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
