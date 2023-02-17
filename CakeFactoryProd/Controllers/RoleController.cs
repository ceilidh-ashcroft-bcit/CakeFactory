using CakeFactoryProd.Data;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        CakeFactoryContext _context;

        public RoleController(CakeFactoryContext context)
        {
            _context = context;
        }

        // GET: Role
        public ActionResult Index()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            return View(roleRepo.GetAllRoles());
        }
    }

}
