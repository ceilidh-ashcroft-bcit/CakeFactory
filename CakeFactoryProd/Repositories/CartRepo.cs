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


    }
}
