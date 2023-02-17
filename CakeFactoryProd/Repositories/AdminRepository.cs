using CakeFactoryProd.Data;

namespace CakeFactoryProd.Repositories
{
    public class AdminRepository
    {
        CakeFactoryContext _context;

        public AdminRepository(CakeFactoryContext context)
        {
            this._context = context;
        }
    }
}
