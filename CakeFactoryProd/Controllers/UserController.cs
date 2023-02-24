using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
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

            CakeOrderRepository cakeOrderRepository = new CakeOrderRepository();
            //var orders = cakeOrderRepo.GetUserOrders(user.Id);

            //int count = cakeOrderRepository.GetAllUserOrders(user.Id);   
            //System.Console.WriteLine("Total number of cakes   "+count);



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



        public IActionResult Edit()
        {

            var email = User.Identity.Name;

            UserRepository userRepo = new UserRepository(_context);
            var user = userRepo.GetUserProfile(email);

            UserVM userVM = new UserVM
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                PrefferedName = user.PreferredName,
                PhoneNumber = user.PhoneNumber,
                


            };

            return View(userVM);

        }

        [HttpPost]

        public IActionResult Edit([Bind("UserId,UserName,Email,PrefferedName,PhoneNumber")] UserVM theaccount)
        {
            UserRepository userRepo = new UserRepository(_context);

            if (ModelState.IsValid){

                userRepo.Edit(new User
                {
                    Id = theaccount.UserId,
                    Name = theaccount.UserName,
                    Email = theaccount.Email,
                    PreferredName = theaccount.PrefferedName,
                    PhoneNumber = theaccount.PhoneNumber,
                    IsActive= theaccount.IsActive,
                });
                
            }

            return RedirectToAction("UserProfile");

        }

        //public IActionResult OrderHistory()
        //{
        //    CakeOrderRepository cakeOrderRepository = new CakeOrderRepository();

        //    var email = User.Identity.Name;

        //    UserRepository userRepo = new UserRepository(_context);
        //    var user = userRepo.GetUserProfile(email);

        //    int count = cakeOrderRepository.GetAllOrders(user.Id);


        //}


    }
    }
