using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CakeFactoryProd.Controllers
{
    //[Authorize]
    public class PaymentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CakeFactoryContext _context;

        public PaymentController(
            ILogger<HomeController> logger,
            CakeFactoryContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        // This method receives and stores
        // the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN ipn)
        {
            try
            {
                _context.IPNs.Add(ipn);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(ipn);
        }


        //public List<IPN> GetAllIPNs()
        //{
        //    //try
        //    //{
        //        var temp = _context.IPNs.ToList();
        //        //return temp;
        //    //}
        //    //catch (SqlException e)
        //    //{
        //    //    Console.WriteLine("###ERROR: " + e.Message);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine("###ERROR: " + ex.Message);
        //    //}
        //    return View(temp);

        //}


        public async Task<IActionResult> Test()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7158/");

                // Set the content type
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the post data
                var postData = new Dictionary<string, string>
                {
                    //Custom data goes here!  2PD58250V9407081E   2023-02-02T01:33:02Z M9CECTMTQJ2M4   Purchase First   Purchase purchaserfakeemail1@tk.ca US  VERIFIED    3.79    CAD sale    paypal approved
                    { "paymentID", "PAYID-MPIH5HA4J269406WS7648845" },
                    {"create_time", DateTime.Now.ToString("dd'/'MM'/'yyyy , HH:mm")},
                    {"payerFirstName", "CackeFactory" },
                    {"amount", "123.46" },
                    {"paymentMethod", "paypal" },
                    {"orderId", "7" }
                };

                // Send the request
                var response = await client.PostAsync("Payment/PaySuccess", new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"));
                var responseContent = await response.Content.ReadAsStringAsync();
                return Confirmation("PAYID-MPIH5HA4J269406WS7648845");
            }
        }
        public IActionResult Confirmation(string confirmationId)
        {
            // check whether there is real data coming,
            // if not, it displays fake data below
            if (confirmationId != null)
            {
                IPN transaction = _context.IPNs
                                    .FirstOrDefault(t => t.PaymentId== confirmationId);

                return View("Confirmation", transaction);
            }

            //this is temporary with hardcoded data
            var temp = new IPN
            {
                PaymentId = "PAYID-MPIH5HA4J269406WS7648845",
                CreateTime = DateTime.Now.ToString("f"),
                PayerFirstName = "CackeFactory - it is error",
                Amount = "123.46",
                PaymentMethod = "paypal"
            };
            ViewBag.temp = "flag is ON";
            return View("Confirmation", temp);
        }


        //need to check by userid or date ?
        //gonna use it?
        public IActionResult Transactions()
        {
            var items = _context.IPNs
                                //.Where(ipn => ipn.UserId == receveid.Id)
                                //.Where(ipn => ipn.create_time > received.start)
                                ;

            //foreach (var item in items)
            //{
            //    DateTime temp = item.CreateTime;
            //    var formatedDate = temp.ToString("dd'/'MM'/'yyyy - HH:mm");
            //    item.CreateTime = formatedDate;
            //}

            return View(items);
        }

    }
}
