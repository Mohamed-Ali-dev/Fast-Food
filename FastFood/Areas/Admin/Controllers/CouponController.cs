using FastFood.Models;
using FastFood.Repository.Repository.IRepository;
using FastFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CouponController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath = "/Images/Coupon";

        public CouponController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var enumValues = Enum.GetValues(typeof(CouponType))
        .Cast<CouponType>()
        .Select(e => new SelectListItem
        {
            Value = e.ToString(), // This is the value sent to the server on form submission
            Text = e.ToString()   // This is the text displayed in the dropdown
        }).ToList();

            ViewBag.CouponTypes = enumValues;

            var coupons = _unitOfWork.Coupon.GetAll();
            return View(coupons);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var coupon = new Coupon();
            return View(coupon);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Coupon coupon,IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    coupon.CouponImage = await SaveFile(file);
                }
              _unitOfWork.Coupon.Add(coupon);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(coupon);
        }
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var coupon = _unitOfWork.Coupon.Get(c => c.Id == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Coupon coupon, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if(coupon.CouponImage == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, coupon.CouponImage.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    coupon.CouponImage = await SaveFile(file);

                }
                _unitOfWork.Coupon.Update(coupon);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(coupon);
        }
        internal async Task<string> SaveFile(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = $"{_webHostEnvironment.WebRootPath}{_imagePath}";
            var imagePath = Path.Combine(filePath, fileName);
            using var stream = System.IO.File.Create(imagePath);
            await file.CopyToAsync(stream);
            return @"\Images\Coupon\" + fileName;
        }
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }
        //    var couponToDelete = _unitOfWork.Coupon.Get(c => c.Id == id);
        //    if (couponToDelete == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(couponToDelete);


        //}
        //[HttpPost,ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    if (id == 0 || id == null)
        //    {
        //        return NotFound();
        //    }
        //    var couponToDelete = _unitOfWork.Coupon.Get(c => c.Id == id);
        //    if (couponToDelete == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Coupon.Remove(couponToDelete);
        //    _unitOfWork.Save();
        //    return RedirectToAction(nameof(Index));
        //}




        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var objToBeDeleted = _unitOfWork.Coupon.Get(u => u.Id == id);
            if (objToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (objToBeDeleted.CouponImage != null)
            {
                var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, objToBeDeleted.CouponImage.TrimStart('\\'));


                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }
            _unitOfWork.Coupon.Remove(objToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });


        }

        #endregion
    }
}
