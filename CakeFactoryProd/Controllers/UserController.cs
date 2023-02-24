using CakeFactoryProd.Data;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace CakeFactoryProd.Controllers
{
    public class UserController : Controller
    {

        CakeFactoryContext _context;

        public UserController(CakeFactoryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult UserProfile()
        {
            var email = User.Identity.Name;

            UserRepository userRepo = new UserRepository(_context);
            var user = userRepo.GetUserProfile(email);

            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository();
            //var orders = cakeOrderRepo.GetUserOrders(user.Id);



            UserVM userVM = new UserVM
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                PrefferedName = user.PreferredName,
                PhoneNumber = user.PhoneNumber,
                //TotalNumberOfOrders = orders.Id

            };
            return View(userVM);

        }
    }
}
