
﻿using CakeFactoryProd.Data;
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
                CakeOrderVM cakeOrderVM = cakeRepo.GetCustomCake();

                return View(cakeOrderVM);
            }

            //Pre made cake view 
            else
            {
                CakeOrderVM cakeOrderVM = cakeRepo.GetCakeById(id);

                return View(cakeOrderVM);
            }
            
        }

        public IActionResult CakeOrderDetail(int id)
        {
            
            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);

            AdminOrderVM adminOrderVM = cakeOrderRepo.GetAdminCakeOrderById(id);

            return View(adminOrderVM);
        
        }


        public IActionResult CakeOrderEdit(int id)
        {
            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);

            AdminOrderVM adminOrderVM = cakeOrderRepo.CakeOrderAdminEdit(id);

            return View(adminOrderVM);
        }

        [HttpPost]
        public IActionResult CakeOrderEdit(Order order)
        {
            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);
            AdminOrderVM adminOrderVM = new AdminOrderVM()
            {
                IsPicked = order.IsPicked,

                CakeOrderVM = new CakeOrderVM
                {
                    OrderId = order.Id
                },
            };

            cakeOrderRepo.CakeOrderAdminUpdate(adminOrderVM);

            return RedirectToAction("Index", "Admin");

        }
    }
}
