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

        [HttpPost]
        public IActionResult Index(IFormCollection pairs)
        public IActionResult Index(CartVM cart)
        {
            return View(cart);
        }


        public void AddToCart(CakeOrderVM cakeOrderVM)
        [HttpPost]
        public IActionResult Index(IFormCollection pairs)
        {
            CakeVM cakeVM = new CakeVM()
            {
                SizeId = Int32.Parse(pairs["Sizes"]),
                ShapeId = Int32.Parse(pairs["Shapes"]),
                FillingId = Int32.Parse(pairs["filling"]),
                ToppingId = Int32.Parse(pairs["topping"]),
                Name = pairs["name"],
                CakeImage = pairs["imagePath"],
                Description = pairs["description"]
            };

            CakeOrderVM cakeOrder = new CakeOrderVM()
            {
                CakeVM =cakeVM,
                /*CustomMessage = pairs["custom-plaque"],*/
                /*PickupDate = DateTime.Parse(pairs["PickupDate"]),*/
                Quantity = Int32.Parse(pairs["quantity-input"])
            };

            CartVM cartVM = new CartVM()
            {
                ID = 1,
                CakeVM = cakeVM,
                OrderVM = cakeOrder
            };

            AddToCart(cartVM);
            var cart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            return View(cart);
        }


        public void AddToCart(CartVM cartVM)
        {
            var currentCart = HttpContext.Session.GetComplexData<List<CakeOrderVM>>("_Cart");
            var currentCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            if (currentCart == null)
            {
                List<CakeOrderVM> list = new List<CakeOrderVM>();
                list.Add(cakeOrderVM);
                List<CartVM> list = new List<CartVM>();
                list.Add(cartVM);
                HttpContext.Session.SetComplexData("_Cart", list);
            }

            else
            {
                currentCart.Add(cakeOrderVM);
                HttpContext.Session.SetComplexData("_Cart", currentCart); 
                currentCart.Add(cartVM);
                HttpContext.Session.SetComplexData("_Cart", currentCart); 
            }

            return;
        }
    }
}
