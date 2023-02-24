using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    public class CakeController : Controller
    {
        private readonly CakeFactoryContext _context;

        public CakeController(CakeFactoryContext context)
        {
            _context= context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id) 
        {
            CakeRepository cakeRepo = new CakeRepository(_context);

            //Custom cake view 
            if (id == 0)
            {
                CakeOrderVM cakeOrderVM = cakeRepo.CreateCustomCake();

                return View(cakeOrderVM);
            }

            //Pre ordered cake view 
            else
            {
                CakeOrderVM cakeOrderVM = cakeRepo.GetCakeById(id);

                return View(cakeOrderVM);
            }
            
        }
    }
}
