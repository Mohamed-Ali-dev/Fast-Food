using FastFood.Models;
using FastFood.Models.ViewModels;
using FastFood.Repository.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastFood.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties:"Item"),
                OrderHeader = new()
            };
            IEnumerable<ItemImage> itemImages = _unitOfWork.ItemImage.GetAll();

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Item.ItemImages = itemImages.Where(u => u.ItemId == cart.Item.Id).ToList();
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);  
            }

            return View(ShoppingCartVM);
        }

      
    }
}
