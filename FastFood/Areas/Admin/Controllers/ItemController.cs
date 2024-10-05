using FastFood.DtOs;
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
        public IActionResult Upsert(int? id)
        {
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
            if(id == null || id  == 0)
            {
                return View(itemVM);

            }
            else
            {
                itemVM.Item = _unitOfWork.Item.Get(u => u.Id == id, includeProperties: "ItemImages");
                return View(itemVM);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(ItemVM itemVM, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                if(itemVM.Item.Id == 0)
                {
                    _unitOfWork.Item.Add(itemVM.Item);
                }
                else
                {
                    _unitOfWork.Item.Update(itemVM.Item);
                }
                _unitOfWork.Save();

                if(files != null)
                {
                    string wwRootPath = _webHostEnvironment.WebRootPath;
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string objPath = @"Images\Items\Item-" + itemVM.Item.Id;
                        string finalPath = Path.Combine(wwRootPath, objPath);
                        if(!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ItemImage itemImage = new() { 
                        ImageUrl = $@"\{objPath}\{fileName}",
                        ItemId = itemVM.Item.Id
                        };
                        if (itemVM.Item.ItemImages == null)
                            itemVM.Item.ItemImages = new List<ItemImage>();

                        itemVM.Item.ItemImages.Add(itemImage);
                    }
                    _unitOfWork.Item.Update(itemVM.Item);
                    _unitOfWork.Save();
                }
                TempData["success"] = "completed successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                itemVM.CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Title,
                    Value= e.Id.ToString(),
                });
                itemVM.SubCategoryList = _unitOfWork.SubCategory.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Title,
                    Value = e.Id.ToString(),
                });
            }
            return View(itemVM);
        }
    
        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.ItemImage.Get(u =>  u.Id == imageId);
            int itemId = imageToBeDeleted.ItemId;
            if(imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    _unitOfWork.ItemImage.Remove(imageToBeDeleted);
                    _unitOfWork.Save();
                    TempData["error"] = "Deleted successfully";
                }
            }
            return RedirectToAction(nameof(Upsert), new { id = itemId });
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _unitOfWork.Item.GetAll(includeProperties: "Category,SubCategory")
          .Select(i => new ItemDto
          {
              Id = i.Id,
              Title = i.Title,
              Description = i.Description,
              Price = i.Price,
              Category = i.Category?.Title, 
              SubCategory = i.SubCategory?.Title 
          }).ToList();

            return Json(new { data = items });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var itemToBeDeleted = _unitOfWork.Item.Get(u => u.Id == id);
            if (itemToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            string itemPath = @"Images\Items\Item-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, itemPath);
            if(Directory.Exists(finalPath) && Directory.EnumerateFiles(finalPath).Any())
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach(string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

           Directory.Delete(finalPath);
            }
       
        
            _unitOfWork.Item.Remove(itemToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
            #endregion
        }
    }
}
