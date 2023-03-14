
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

            CakeOrderVM cakeOrderVM = cakeOrderRepo.GetCakeOrderById(id);

            return View(cakeOrderVM);
        
        }

        public IActionResult Create()
        {
         return View();
        }
    }
}
