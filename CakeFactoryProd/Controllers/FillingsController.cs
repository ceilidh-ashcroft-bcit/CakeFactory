using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FillingVM fillingVM)
        {
               FillingsRepo fillingsRepo= new FillingsRepo(_context);
                var response = fillingsRepo.CreateNewFilling(fillingVM);
                return RedirectToAction("Index"); ;

        }

        public IActionResult Delete(int id)
        {
            FillingsRepo fillingsRepo = new FillingsRepo(_context);
            var filling = fillingsRepo.GetFillingById(id);
            return View(filling);
        }

        [HttpPost]
        public IActionResult Delete(Filling filling)
        {
                FillingsRepo fillingsRepo = new FillingsRepo(_context);
                var response = fillingsRepo.DeleteFillingById(filling.Id);
                return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            FillingsRepo fillingsRepository = new FillingsRepo(_context);
            var filling = fillingsRepository.GetFillingById(id);
            return View(filling);
        }

        public IActionResult Edit(int id)
        {
            FillingsRepo fillingsRepo = new FillingsRepo(_context);
            var filling = fillingsRepo.GetFillingById(id);
            return View(filling);
        }

        [HttpPost]

        public IActionResult Edit(Filling filling)
        {
            FillingsRepo fillingsRepo = new FillingsRepo(_context);
            FillingVM fillingVM = new FillingVM()
            {
                Flavor = filling.Flavor,
                PriceFactor = filling.PriceFactor,
                FillingId = filling.Id,
                Description = filling.Description,
                IsActive = filling.IsActive
            };
            var response = fillingsRepo.UpdateFillingByID(fillingVM);
            return RedirectToAction("Details", new {id = filling.Id});
        }
    }
}
