using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CakeFactoryProd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CakeFactoryContext _context;

        public HomeController(ILogger<HomeController> logger, CakeFactoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                UserRepository userRepo = new UserRepository(_context);
                User user = userRepo.GetUserByEmail(User.Identity.Name);
                HttpContext.Session.SetString("Preffered Name", user.PreferredName);
            }

            CakeRepository cakeRepo = new CakeRepository(_context);
            var allCakes = cakeRepo.GetAllActivePredefinedCakes();
            return View(allCakes);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}