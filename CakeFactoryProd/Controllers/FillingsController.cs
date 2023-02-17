using CakeFactoryProd.Data;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class FillingsController : Controller
    {
        private readonly CakeFactoryContext _context;

        public FillingsController(CakeFactoryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            FillingsRepo fillingsRepo = new FillingsRepo(_context);
            var fillingsList = fillingsRepo.GetAllFillings();
            return View(fillingsList);
        }
    }
}
