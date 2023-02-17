using CakeFactoryProd.Data;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Repositories
{
    public class CakeRepository
    {
        public CakeRepository(CakeFactoryContext context)
        {
            _context = context;
        }

        private readonly CakeFactoryContext _context;

        public List<CakeVM> GetCakesAll()
        {

            var cakes = from c in _context.Cakes
                     join sz in _context.Sizes on c.SizeId equals sz.Id
                     join sp in _context.Shapes on c.ShapeId equals sp.Id
                     select new CakeVM
                     {
                         Name = c.Name,
                         Price = c.Price,
                         IsActive = c.IsActive,
                         Size = sz.Value,
                         Shape = sp.Value
                     };
            /*IQueryable<CakeVM> cakes = _context.Cakes.Select(c => new CakeVM { Name = c.Name, Price = c.Price, IsActive = c.IsActive, Shape = c.ShapeId, Size = c.SizeId });
*/
            return cakes.ToList();
        }
    }
}
