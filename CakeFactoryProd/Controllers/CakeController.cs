using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class CakeController1cs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
