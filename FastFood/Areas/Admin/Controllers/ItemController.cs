using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Repository;
using FastFood.Repository.Repository.IRepository;
using FastFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]


    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath = "/Images/Item";


        public ItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var items = _unitOfWork.Item.GetAll(includeProperties: "Category,SubCategory")
                .Select(model => new ItemVM()
                {
                    Item = model
                }).ToList();
            return View(items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Item item = new Item();
            ItemVM itemVM = new ItemVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }),
                SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(s => new SelectListItem
                {
                    Text = s.Title,
                    Value = s.Id.ToString()
                }),
                Item = new Item()

            };
            return View(itemVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemVM itemVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    itemVM.Item.ImageUrl = await SaveFile(file);
                }

                _unitOfWork.Item.Add(itemVM.Item);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(itemVM);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Item item = _unitOfWork.Item.Get(i => i.Id == id, includeProperties: "Category,SubCategory");
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                ItemVM itemVM = new ItemVM()
                {
                    Item = item,
                    CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Title,
                        Value = c.Id.ToString()
                    }),
                    SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(s => new SelectListItem
                    {
                        Text = s.Title,
                        Value = s.Id.ToString()
                    })

                };
                return View(itemVM);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ItemVM itemVM, IFormFile file)
        {

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log or handle errors
                    Console.WriteLine(error.ErrorMessage);
                }
                itemVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });
                itemVM.SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(s => new SelectListItem
                {
                    Text = s.Title,
                    Value = s.Id.ToString()
                });
                return View(itemVM);

            }
            else
            {

                if (file != null && file.Length > 0)
                {

                    Item itemFromDb = _unitOfWork.Item.Get(u => u.Id == itemVM.Item.Id);


                    if (itemFromDb == null)
                    {
                        return NotFound();
                    }
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, itemFromDb.ImageUrl.TrimStart('\\'));


                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    itemVM.Item.ImageUrl = await SaveFile(file);

                }

                _unitOfWork.Item.Update(itemVM.Item);
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }


        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = $"{_webHostEnvironment.WebRootPath}{_imagesPath}";
            var imagePath = Path.Combine(filePath, fileName);// wwwroot/Images/item/guidstring.extention
            using var stream = System.IO.File.Create(imagePath);
            await file.CopyToAsync(stream);
            return @"\Images\Item\" + fileName;

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Item> items = _unitOfWork.Item.GetAll(includeProperties: "Category,SubCategory").ToList();
            return Json(new { data = items });
        }
        //[HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var itemToBeDeleted = _unitOfWork.Item.Get(u => u.Id == id);
            if (itemToBeDeleted == null)
            {
                return NotFound();
            }
            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, itemToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }
            _unitOfWork.Item.Remove(itemToBeDeleted);
            _unitOfWork.Save();
            return Ok();
            #endregion
        }
    }
}
