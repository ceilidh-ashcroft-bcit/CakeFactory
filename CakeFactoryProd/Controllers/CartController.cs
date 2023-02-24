using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CakeFactoryProd.Controllers
{
    public class CartController : Controller
    {
        private readonly CakeFactoryContext _context;
        public CartController(CakeFactoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetComplexData<List<CakeVM>>("_Cart");
            return View(cart);
        }

        [HttpPost]
        public void AddToCart(CakeVM cakeVM)
        {
            var currentCart = HttpContext.Session.GetComplexData<List<CakeVM>>("_Cart");
            if (currentCart == null)
            {
                List<CakeVM> list = new List<CakeVM>();
                list.Add(cakeVM);
                HttpContext.Session.SetComplexData("_Cart", list);
            }

            else
            {
                currentCart.Add(cakeVM);
                HttpContext.Session.SetComplexData("_Cart", currentCart);
            }

            return;
        }
    }
}
