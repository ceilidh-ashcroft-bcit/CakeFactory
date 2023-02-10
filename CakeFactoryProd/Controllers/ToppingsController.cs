using CakeFactoryProd.Data;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class ToppingsController : Controller
    {
        private readonly CakeFactoryContext _context;

        public ToppingsController(CakeFactoryContext context) { _context = context; }
        public IActionResult Index()
        {
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            var toppings = toppingsRepo.GetAllToppings();
            return View(toppings);
        }
    }
}
