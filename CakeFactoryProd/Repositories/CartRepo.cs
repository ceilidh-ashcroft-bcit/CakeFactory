using CakeFactoryProd.Data;

namespace CakeFactoryProd.Repositories
{
    public class CartRepo
    {
        private readonly CakeFactoryContext _context;
        public CartRepo(CakeFactoryContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetDetails(int shapeID, int sizeID, int fillingID)
        {
            var size = _context.Sizes.Where(s=> s.Id == sizeID).FirstOrDefault();
            var shape = _context.Shapes.Where(s=> s.Id == shapeID).FirstOrDefault();
            var filling = _context.Fillings.Where(f => fillingID == f.Id).FirstOrDefault();
            /*var topping = _context.Toppings.Where(t => t.Id == toppingID).FirstOrDefault();*/

            return new Dictionary<string, string>
            {
                { "Shape", shape.Value },
                { "Size",size.Value },
                { "Filling", filling.Flavor }
                /*{ "Topping", topping.Flavor }*/
            };
        }
    }
}
