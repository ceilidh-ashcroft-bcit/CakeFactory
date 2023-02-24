using CakeFactoryProd.Data;

namespace CakeFactoryProd.Repositories
{
    public class CakeOrderRepository
    {


        CakeFactoryContext _context;

        public CakeOrderRepository()
        {
        }

        public CakeOrderRepository(CakeFactoryContext context)
        {
            _context = context;
        }


        public int GetAllUserOrders(int id)
        {

            var count = _context.Orders.Where(o => o.UserId == id).Count();

            if (count == 0 || count == null)
            {
                return 0;
            }

            //  var countOrders = _context.Orders.Where(o => o.UserId == id).FirstOrDefault();
            return count;
        }
    }
}
