using CakeFactoryProd.Models;

namespace CakeFactoryProd.Repositories
{
    /// <summary>
    /// Class responsable for the ShapeReposiory and all its methods to the DB.
    /// </summary>
    public class ShapeRepository
    {
        public Shape GetShapeById(int id)
        {
            CakeFactoryContext db = new CakeFactoryContext();
            var shape = db.Shapes
                            .Where(s => s.Id == id)
                            .FirstOrDefault();

            return shape;
        }

        public List<Shape> GetAllShapes()
        {
            CakeFactoryContext db = new CakeFactoryContext();

            return db.Shapes.ToList();
        }

        public string CreateShape(Shape newShape)
        {
            //// add tuple and return message + new shape item

            var message = "";
            try
            {
                CakeFactoryContext db = new CakeFactoryContext();

                db.Add(newShape);
                db.SaveChanges();

                message = "New shape has been added succesfully!";

            }
            catch (Exception error)
            {
                message = $"#Error ShapeC-01: {error.Message}";
                Console.WriteLine(message);
            }

            return message;
        }

        public string UpdateShape(Shape updateShape)
        {
            ////// also need to return tuple - string + object
            var message = "";
            try
            {
                CakeFactoryContext db = new CakeFactoryContext();

                db.Update(updateShape);
                db.SaveChanges();

                message = "The update has been done successfully.";
            }
            catch (Exception err)
            {
                message = $"#Error ShapeU-02: {err.Message}";
            }

            return message;
        }

    }
}
