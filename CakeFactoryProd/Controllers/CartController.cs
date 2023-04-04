using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CakeFactoryProd.Controllers
{
    public class CartController : Controller
    {
        private readonly CakeFactoryContext _context;
        private readonly IConfiguration _configuration;
        public CartController(
            CakeFactoryContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            //if ()
            var tempCakeID = Int32.Parse(pairs["CakeId"]);
            CartRepo cartRepo = new CartRepo(_context);
            Dictionary<string, string> properties = cartRepo.GetDetails(Int32.Parse(pairs["Shapes"]),
                                                                                Int32.Parse(pairs["Sizes"]),
                                                                                Int32.Parse(pairs["Fillings"])
                                                                                /*Int32.Parse(pairs["Toppings"])*/
                                                                                );

            CakeVM cakeVM = new CakeVM()
            {
                CakeId= tempCakeID,
                SizeId = Int32.Parse(pairs["Sizes"]),
                Size = properties["Size"],
                ShapeId = Int32.Parse(pairs["Shapes"]),
                Shape = properties["Shape"],
                FillingId = Int32.Parse(pairs["Fillings"]),
                Filling = properties["Filling"],
                ToppingList = (pairs["Toppings"]),
                Name = pairs["name"],
                ImageName = pairs["imagePath"],
                Description = pairs["description"]
            };

            CakeOrderVM cakeOrder = new CakeOrderVM()
            {
                CakeVM = cakeVM,
                CustomMessage = pairs["custom-plaque"],
                PickupDate = DateTime.Parse(pairs["PickupDate"]),
                Quantity = Int32.Parse(pairs["quantity-input"]),
                Total = Decimal.Parse(pairs["total"])
            };

            var currentCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            int newID = 0;

            if (currentCart != null && currentCart.Count > 0)
            {
                newID = currentCart.Last().ID +1;
            }

            CartVM cartVM = new CartVM()
            {
                ID = newID,
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


        [HttpPost]
        //public IActionResult Confirmation([FromBody] IPN ipn)
        public JsonResult Confirmation([FromBody] IPN ipn)
        {
            try
            {
                // it simulates an error after the payment is done
                //if (1 == 1) 
                //    throw new NullReferenceException("ERROR test.");

                var email = User.Identity.Name;
                var currentCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
                CartRepo cartRepo = new CartRepo(_context);

                int orderNumber = cartRepo.CreateOrder(currentCart, email, ipn);
                HttpContext.Session.SetComplexData("_Cart", new List<CartVM>());

                var temp = Json(new
                {
                    Status = "Success",
                    orderId = orderNumber
                });

                return(temp);
            } catch(Exception ex)
            {
                // because the payment was already processed,
                // cart is going to be removed
                HttpContext.Session.SetComplexData("_Cart", new List<CartVM>());

                return Json(ex.Message);
            }
        }

        public async Task<IActionResult> Success(int orderId)
        {
            try
            {
                OrderRepository or = new OrderRepository(_context);
                var order = or.GetOrderById(orderId);
                CartRepo cartRepo = new CartRepo(_context);
                var ipn = cartRepo.GetIPNDetailsByOrderId(order.Id);
                UserRepository ur = new UserRepository(_context);
                var user = ur.GetUserProfileById(order.UserId);

                var emailInfo = new EmailVM()
                {
                    OrderId = order.Id,
                    Email = user.Email,
                    Name = user.Name,
                    TotalAmount = order.TotalAmount,
                    PurchaseDate = order.PurchaseDate,
                    PickupDate = order.PickupDate
                };

                await PurchaseEmail(emailInfo);

                return View(ipn);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> PurchaseEmail(EmailVM emailInfo)
        {
            try
            {
                var sendGridApiKey = _configuration.GetSection("SendGrid")["ApiKey"];
                var sendGridClient = new SendGridClient(sendGridApiKey);
                var from = new EmailAddress("ssd.team.orange@gmail.com", "Team Orange");
                var subject = "Cake Factory - Purchase Success";
                var to = new EmailAddress(emailInfo.Email, emailInfo.Name);

                var purchaseDate = emailInfo.PurchaseDate?.ToString("dddd, dd MMMM yyyy");
                var pickupDate = emailInfo.PickupDate?.ToString("dddd, dd MMMM yyyy");
                var totalAmount = emailInfo.TotalAmount.ToString("0.00");

                var plainContent = "Congratulation!";
                var msgTitle = $"<h2>Hello {emailInfo.Name}</h2><br>";
                var msgBody = "<p>Thank you for purchasing with Cake Factory!</p><p>We received your order and we will make it a delicous experience.</p>";
                var msgInfo = $"<b><span>Order #: {emailInfo.OrderId}</span><br><span>Purchase Date: {purchaseDate}</span><br><span>Pick up Date: {pickupDate}</span><br><span>Total $: CAD {totalAmount}</span></b>";
                var msgFooter = $"<p>Thank you. :)</p><p style=\"color:blue;\"><b>Cake Factory</b></p>";
                var htmlContent = msgTitle + msgBody + msgInfo + msgFooter;
                
                var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
                await sendGridClient.SendEmailAsync(mailMessage);
                
                return Ok();
            } catch (Exception ex)
            {
                return View("Error");
            }
        }


        public IActionResult Error()
        {
            // user is going to be redirect to an Error page
            // with information to contact Cake Factory
            return View("Error");
        }

        public IActionResult Delete(int id)
        {
            var currentCart = HttpContext.Session.GetComplexData<List<CartVM>>("_Cart");
            var newCart = currentCart.Except(currentCart.Where(c=> c.ID == id));

            HttpContext.Session.SetComplexData("_Cart", newCart);

            return View("Index", newCart);
        }
    }
}
