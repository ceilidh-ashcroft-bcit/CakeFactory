using CakeFactoryProd.Data;
using Microsoft.AspNetCore.Mvc;

namespace CakeFactoryProd.Controllers
{
    /// <summary>
    /// Class responsable for the Shape Controller and all its methods.
    /// </summary>
    public class ShapeController : Controller
    {
        private readonly CakeFactoryContext _db;

        public ShapeController( CakeFactoryContext context )
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
