using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CakeFactoryProd.Controllers
{
    public class ShapeController : Controller
    {
        public IActionResult Index()
        {
            var shape = new ShapeRepository();

            return View(shape.GetAllShapes());
        }

        public IActionResult Details(int id)
        {
            var shape = new ShapeRepository();

            return View(shape.GetShapeById(id));
        }

        public IActionResult Create()
        {
            CakeFactoryContext db = new CakeFactoryContext();
            //Console.WriteLine("=================: " + db.Manufacturers);
            //ViewData["Mfg"] = new SelectList(db.Manufacturers, "Mfg", "Mfg");
            //ViewData["Vendor"] = new SelectList(db.Suppliers, "Vendor", "Vendor");

            return View();
        }
    }
}
