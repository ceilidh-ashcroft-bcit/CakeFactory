using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToppingVM toppingVM) 
        {
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            var response = toppingsRepo.CreateNewTopping(toppingVM);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            ToppingsRepo toppingsRepo  = new ToppingsRepo(_context);
            var topping = toppingsRepo.GetToppingById(id);
            return View(topping);
        }

        [HttpPost]
        public IActionResult Delete(Topping topping)
        {
                ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
                var response = toppingsRepo.DeleteToppingById(topping.Id);
                return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            var topping = toppingsRepo.GetToppingById(id);
            return View(topping);
        }

        public IActionResult Edit(int id)
        {
            ToppingsRepo toppingsRepository = new ToppingsRepo(_context);
            var topping = toppingsRepository.GetToppingById(id);
            return View(topping);
        }

        [HttpPost]

        public IActionResult Edit(Topping topping)
        {
            ToppingsRepo toppingsRepo = new ToppingsRepo(_context);
            ToppingVM toppingVM = new ToppingVM()
            {
                Flavor = topping.Flavor,
                PriceFactor = topping.PriceFactor,
                Id = topping.Id,
                Description = topping.Description,
                IsActive = topping.IsActive
            };
            var response = toppingsRepo.UpdateToppingByID(toppingVM);
            return RedirectToAction("Details", new { id = topping.Id });
        }
    }
}
