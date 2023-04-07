using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.Utilities;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace CakeFactoryProd.Controllers
{
    public class UserController : Controller
    {

        CakeFactoryContext _context;
        IServiceProvider _serviceProvider;

        public UserController(CakeFactoryContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
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

            CakeOrderRepository cakeOrderRepository = new CakeOrderRepository(_context);
           
            var orders = cakeOrderRepository.GetAllOrders(user.Id);   
          
            UserVM userVM = new UserVM
            {
                UserId = user.Id,
                UserName = user.Name,
                Email = user.Email,
                PrefferedName = user.PreferredName,
                PhoneNumber = user.PhoneNumber,
                TotalNumberOfOrders = orders.Count,

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
        //    CakeOrderRepository cakeOrderRepository = new CakeOrderRepository(_context);

        //    var email = User.Identity.Name;

        //    UserRepository userRepo = new UserRepository(_context);
        //    var user = userRepo.GetUserProfile(email);

        //    var orders = cakeOrderRepository.GetAllOrders(user.Id);

        //    if (orders.Count == 0)
        //    {
        //        return View();
        //    }
        //    return View(orders);


        //}

        public IActionResult OrderHistory(string sortOrder, string currentFilter, string searchString, int? page)
        {

            // searching

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            CakeOrderRepository cakeOrderRepository = new CakeOrderRepository(_context);

            var email = User.Identity.Name;

            UserRepository userRepo = new UserRepository(_context);
            var user = userRepo.GetUserProfile(email);

            IQueryable<CakeOrderVM> cakeOrderVM = cakeOrderRepository.GetAllOrders(user.Id).AsQueryable();

            // pagination code
            int pageSize = 10;
                return View(PaginatedList<CakeOrderVM>.Create(cakeOrderVM.AsNoTracking(), page ?? 1, pageSize));


                //return View(orders);
        }


        /*******************    ADMIN CURD OPERATONS  ****************/

        public IActionResult Details(int id)
        {
            UserRepository userRepo = new UserRepository(_context);
            var user = userRepo.GetUserProfileById(id);

            UserVM userVM = new UserVM
            {
                UserId = user.Id,
                UserName = user.Name,
                PrefferedName = user.PreferredName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsActive = (bool)user.IsActive
            };
            return View(userVM);
        }



        public IActionResult EditUserByAdmin(int id)
        {

            UserRepository userRepo = new UserRepository(_context);
            var user = userRepo.GetUserProfileById(id);

            UserAdminVM userAdminVM = new UserAdminVM
            {
                UserId = user.Id,
                UserName = user.Name,
                PrefferedName = user.PreferredName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsActive = (bool)user.IsActive,
                

            };

            return View(userAdminVM);

        }

        [HttpPost]

        public IActionResult EditUserByAdmin([Bind("UserId,UserName,Email,PrefferedName,PhoneNumber,IsActive")] UserVM theaccount)
        {
            UserRepository userRepo = new UserRepository(_context);

            if (ModelState.IsValid)
            {

                userRepo.Edit(new User
                {
                    Id = theaccount.UserId,
                    Name = theaccount.UserName,
                    Email = theaccount.Email,
                    PreferredName = theaccount.PrefferedName,
                    PhoneNumber = theaccount.PhoneNumber,
                    IsActive = theaccount.IsActive,
                });

            }

            return RedirectToAction("Users", "Admin");

        }


        public IActionResult Delete(int id)
        {
            UserRepository userRepository = new UserRepository(_context);
            if (ModelState.IsValid)
            {
                userRepository.DeleteUser(id);
            }

            return RedirectToAction("Users", "Admin");
        }

     



    }
}
