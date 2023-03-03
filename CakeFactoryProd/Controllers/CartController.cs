using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
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


        public IActionResult Index(List<CartVM> cart)
        {
            if (cart == null || cart.Count == 0)
            {
                var sessionCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
                if (sessionCart != null)
                {
                    cart = sessionCart;
                }
            }
            return View(cart);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection pairs)
        {
            CartRepo cartRepo = new CartRepo(_context);
            Dictionary<string, string> properties = cartRepo.GetDetails(Int32.Parse(pairs["Shapes"]),
                                                                                Int32.Parse(pairs["Sizes"]),
                                                                                Int32.Parse(pairs["Fillings"])
                                                                                /*Int32.Parse(pairs["Toppings"])*/
                                                                                );

            CakeVM cakeVM = new CakeVM()
            {
                SizeId = Int32.Parse(pairs["Sizes"]),
                Size = properties["Size"],
                ShapeId = Int32.Parse(pairs["Shapes"]),
                Shape = properties["Shape"],
                FillingId = Int32.Parse(pairs["Fillings"]),
                Filling = properties["Filling"],
                ToppingId = Int32.Parse(pairs["Toppings"]),
                Name = pairs["name"],
                CakeImage = pairs["imagePath"],
                Description = pairs["description"],
            };

            CakeOrderVM cakeOrder = new CakeOrderVM()
            {
                CakeVM =cakeVM,
                CustomMessage = pairs["custom-plaque"],
                PickupDate = DateTime.Parse(pairs["PickupDate"]),
                Quantity = Int32.Parse(pairs["quantity-input"])
                /*Total =*//**/ 
            };

            CartVM cartVM = new CartVM()
            {
                ID = 1,
                CakeVM = cakeVM,
                OrderVM = cakeOrder
            };

            AddToCart(cartVM);
            List<CartVM> cart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            return View("Index", cart);
        }


        public void AddToCart(CartVM cartVM)
        {
            var currentCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            if (currentCart == null)
            {
                List<CartVM> list = new List<CartVM>();
                list.Add(cartVM);
                HttpContext.Session.SetComplexData("_Cart", list);
            }

            else
            {
                currentCart.Add(cartVM);
                HttpContext.Session.SetComplexData("_Cart", currentCart); 
            }

            return;
        }
    }
}
