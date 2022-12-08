using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cakes()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Sales()
        {
            return View();
        }
    }
}
