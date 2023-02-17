using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CakeFactoryProd.Controllers
{
    /// <summary>
    /// Class responsable for the Shape Controller and all its methods.
    /// </summary>
    public class ShapeController : Controller
    {
        private readonly CakeFactoryContext _db;

        public ShapeController(CakeFactoryContext context)
        {
            _db = context;
        }



        /// <summary>
        /// Method responsable for landing the Index Shape page
        /// </summary>
        /// <param name="message">Optionally receives a message to be displayed</param>
        /// <returns>All Shape items in DB</returns>
        public IActionResult Index(string message = "")
        {
            var shape = new ShapeRepository(_db);

            ViewBag.Message = message;

            return View(shape.GetAllShapes());
        }



        /// <summary>
        /// Method that displays details about a particular Shape item
        /// </summary>
        /// <param name="id">Shape's DB id</param>
        /// <returns>View with all Shape item information</returns>
        public IActionResult Details(int id)
        {
            var shape = new ShapeRepository(_db);

            return View(shape.GetShapeById(id));
        }



        /// <summary>
        /// Method that displays the form for creating new Shape record
        /// </summary>
        /// <returns>View form</returns>
        public IActionResult Create()
        {
            
            return View();
        }


        /// <summary>
        /// Method responsable for receiving new Shape object, validate it,
        /// and add to the DB calling the appropriate repository for that.
        /// </summary>
        /// <param name="newShape">Shape object</param>
        /// <returns>Index View with appropiate message</returns>
        [HttpPost]
        public IActionResult Create([Bind("Value, PriceFactor, Description, IsActive")] Shape newShape)
        {
            /*
            // have to validate user/admin
            */

            if (ModelState.IsValid)
            {
                var sr = new ShapeRepository(_db);
                var result = sr.CreateShape(newShape);

                return RedirectToAction("Index", "Shape",
                    new { message = result });
            }


            return View(newShape);
        }



        /// <summary>
        /// Method reponsable for calling the delete form for a particular Shape record
        /// </summary>
        /// <param name="id">Shape DB's id</param>
        /// <returns>View Form delete confirmation</returns>
        public IActionResult Delete(int id, string message = "")
        {
            /*
             * check whether it is an admin user 
             */

            var sr = new ShapeRepository(_db);
            var shapeToBeDeleted = sr.GetShapeById(id);

            ViewBag.Message = message;

            // to be used by delete (Activate/Deativate)
            var isActive = shapeToBeDeleted.IsActive ?? false;
            ViewBag.Action = isActive
                ? "Deactivate"
                : "Activate";

            return View(shapeToBeDeleted);
        }



        /// <summary>
        /// Method for deactivate/activate a Shape item
        /// </summary>
        /// <param name="id">Shape id</param>
        /// <returns>Redirects to the specific View, regarding the result from the operation</returns>
        [HttpPost]
        //public IActionResult Delete([Bind("Value, PriceFactor, Description, IsActive")] Shape shape)
        public IActionResult DeleteShape(int id)
        {
            /*
            // have to validate user/admin
            */

            var sr = new ShapeRepository(_db);
            var removeShape = sr.DeleteShape(id);

            if (removeShape.Item1 == true)
                return RedirectToAction("Index", "Shape",
                    new { message = removeShape.Item2 });

            else
                return RedirectToAction("Delete", "Shape",
                    new { message = removeShape.Item2 });
        }



        /// <summary>
        /// Method which displays a form for Shape Edit.
        /// The form is filled with current Shape's data.
        /// </summary>
        /// <param name="id">Shape id</param>
        /// <param name="value">Value</param>
        /// <param name="priceFactor">Price Factor</param>
        /// <param name="description">Description</param>
        /// <param name="isActive">Is Active</param>
        /// <returns></returns>
        public IActionResult Edit(int id, string message = "")
        {
            /*
            // have to validate user/admin
            */

            var sr = new ShapeRepository(_db);
            var editShape = sr.GetShapeById(id);
            
            ViewBag.Message = message;

            return View(editShape);
        }



        /// <summary>
        /// Method responsable for execute the request for changing Shape data.
        /// It will call Shape repo and pass the new data to it and handle the
        /// response.
        /// </summary>
        /// <param name="updateAccountInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(
            [Bind("Id,Value,PriceFactor,Description, IsActive")]
            Shape updateShape)
        {

            /*
            // have to validate user/admin
            */

            var returnMessage = "";

            if (ModelState.IsValid)
            {
                var shapeToBeUpdated = new Shape()
                {
                    Id = updateShape.Id,
                    Value = updateShape.Value,
                    PriceFactor = updateShape.PriceFactor,
                    Description = updateShape.Description,
                    IsActive = updateShape.IsActive
                };

                var sr = new ShapeRepository(_db);
                var updateShapeResult = sr.UpdateShape(shapeToBeUpdated);


                if (updateShapeResult.Item1)
                    return RedirectToAction("Index", "Shape",
                        new
                        {
                            message = updateShapeResult.Item2
                        });
                else
                    returnMessage = updateShapeResult.Item2;
            }

            returnMessage = returnMessage == ""
                                ? "Error on Updating Shape"
                                : returnMessage;

            return RedirectToAction("Edit", "Shape",
                new { 
                    id = updateShape.Id,
                    message = returnMessage 
                });
        }
    }
}
