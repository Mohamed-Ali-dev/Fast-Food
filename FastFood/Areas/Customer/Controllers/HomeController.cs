using FastFood.Models;
using FastFood.Repository.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FastFood.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
         
            _logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> items = _unitOfWork.Item.GetAll(includeProperties: "Category,SubCategory");
            return View(items);

        }
        public IActionResult Details(int? id)
        {
            Item item = _unitOfWork.Item.Get(u=> u.Id == id, includeProperties: "Category,SubCategory");
            return View(item);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
