using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
