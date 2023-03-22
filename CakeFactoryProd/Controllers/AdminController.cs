using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using CakeFactoryProd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CakeFactoryProd.Utilities;

namespace CakeFactoryProd.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        public AdminController(CakeFactoryContext context)
        {
            _context = context;
        }

        private readonly CakeFactoryContext _context;

        //public IActionResult Index()
        //{
        //    CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);

        //    List<AdminOrderVM> adminOrderVM = cakeOrderRepo.GetAllAdminCakeOrders();
      
        //    return View(adminOrderVM);
        //}

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CakeOrderRepository cakeOrderRepo = new CakeOrderRepository(_context);

            IQueryable<AdminOrderVM> adminOrderVM = cakeOrderRepo.GetAllAdminCakeOrders().AsQueryable();


            if (!String.IsNullOrEmpty(searchString))
            {
                adminOrderVM = adminOrderVM.Where(x => x.UserVM.PrefferedName.Contains(searchString));
                
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                ViewData["OrderSortParm"] = "PickupDate";
            }
            else
            {
                ViewData["OrderSortParm"] = sortOrder == "PickupDate" ?
                                                        "Pickup_desc" : "PickupDate";
            }

            switch (sortOrder)
            {
                case "PickupDate":
                    adminOrderVM = adminOrderVM.OrderByDescending(c => c.CakeOrderVM.PickupDate);
                    break;
                case "Pickup_desc":
                    adminOrderVM = adminOrderVM.OrderBy(p => p.CakeOrderVM.PickupDate);
                    break;
                default:
                    adminOrderVM = adminOrderVM.OrderByDescending(c => c.CakeOrderVM.PickupDate);
                    break;
            }

            int pageSize = 10;
            return View(PaginatedList<AdminOrderVM>.Create(adminOrderVM.AsNoTracking(), page ?? 1, pageSize));

        }

        public IActionResult Cakes()
        {

            CakeRepository cakeRepo = new CakeRepository(_context);
            ToppingsRepo toppingRepo = new ToppingsRepo(_context);
            FillingsRepo fillingRepo = new FillingsRepo(_context);

            CakeEditVM cakeEditVm = new CakeEditVM
            {
                CakeVMs = cakeRepo.GetCakesAll(),
                ToppingVMs = toppingRepo.GetToppingAll(),
                FillingVMs = fillingRepo.GetFillingAll()
            };
            return View(cakeEditVm);
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Sales()
        {
            return View();
        }
    }
}
