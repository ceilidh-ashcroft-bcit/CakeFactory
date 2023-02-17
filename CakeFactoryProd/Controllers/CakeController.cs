using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
