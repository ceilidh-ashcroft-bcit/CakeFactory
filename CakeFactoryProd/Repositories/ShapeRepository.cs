using CakeFactoryProd.Models;
using CakeFactoryProd.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CakeFactoryProd.Repositories
{
    /// <summary>
    /// Class responsable for the ShapeReposiory and all its methods to the DB.
    /// </summary>
    public class ShapeRepository
    {
        private readonly CakeFactoryContext _db;

        public ShapeRepository(CakeFactoryContext context)
        {
            _db = context;
        }

        public Shape GetShapeById(int id)
        {
            var shape = _db.Shapes
                            .Where(s => s.Id == id)
                            .FirstOrDefault();

            return shape;
        }

        public List<Shape> GetAllShapes()
        {
            return _db.Shapes.ToList();
        }



        /// <summary>
        /// Method reponsable for adding a new Shape object into the DB
        /// </summary>
        /// <param name="newShape">Shape object</param>
        /// <returns>message</returns>
        public string CreateShape(Shape newShape)
        {
            //// add tuple and return message + new shape item

            var message = "";
            try
            {
                _db.Add(newShape);
                _db.SaveChanges();

                message = "New shape has been added succesfully!";
            }
            catch (Exception error)
            {
                message = $"#Error ShapeCT-01: {error.Message}";
                Console.WriteLine(message);
            }

            return message;
        }



        /// <summary>
        /// Methodo reponsable to delete a shape item - actually, it turns
        /// the flag IsActive to false
        /// </summary>
        /// <param name="shapeId">Shape id</param>
        /// <returns>Message to confirm the procedure</returns>
        public Tuple<bool, string> DeleteShape(int shapeId)
        {
            var message = "";
            try
            {
                var shapeToBeDeleted = GetShapeById(shapeId);
                shapeToBeDeleted.IsActive = !shapeToBeDeleted.IsActive;
                
                _db.Update(shapeToBeDeleted);
                _db.SaveChanges();

                var result = shapeToBeDeleted.IsActive == true
                                ? "activated"
                                : "deactivated";

                return Tuple.Create(
                    true, $"Shape item has been {result} successfully.");
            }
            catch (Exception err)
            {
                message = $"#Error ShapeDL-01: {err.Message}";
                Console.WriteLine($"Error: {message}");

                return Tuple.Create(false, message);
            }
        }



        public Tuple<bool, string> UpdateShape(Shape updateShape)
        {
            try
            {
                _db.Update(updateShape);
                _db.SaveChanges();

                return Tuple.Create(
                    true, $"Shape item has been Updated successfully.");
            }
            catch (Exception err)
            {
                var message = $"#Error ShapeUP-01: {err.Message}";
                Console.WriteLine(message);

                return Tuple.Create(false, message);
            }
        }
    }
}
