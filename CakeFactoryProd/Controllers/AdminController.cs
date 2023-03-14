using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
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
            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);

            List<AdminOrderVM> adminOrderVM = cakeOrderRepo.GetAllCakeOrders();
      
            return View(adminOrderVM);
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
            //var email = User.Identity.Name;

            UserRepository userRepository = new UserRepository(_context);
            var users = userRepository.GetAllUsers();
            return View(users);
        }
        public IActionResult Sales()
        {

            return View();
        }
    }
}
