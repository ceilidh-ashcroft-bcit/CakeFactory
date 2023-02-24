using CakeFactoryProd.Data;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        public AdminController(CakeFactoryContext context)
        {
            _context = context;
        }

        private readonly CakeFactoryContext _context;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cakes()
        {

            CakeRepository cakeRepo = new CakeRepository(_context);
            ToppingsRepo toppingRepo = new ToppingsRepo(_context);
            FillingsRepo fillingRepo = new FillingsRepo(_context);

            CakeEditVM cakeEditVm = new CakeEditVM
            {
                CakeVMs = cakeRepo.GetCakesAll(),
                ToppingVMs = toppingRepo.GetToppingAll(),
                FillingVMs = fillingRepo.GetFillingAll()
            };
            return View(cakeEditVm);
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
