using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class ToppingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
