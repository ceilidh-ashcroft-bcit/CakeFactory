using CakeFactoryProd.Models;
using CakeFactoryProd.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CakeFactoryProd.Repositories
{
    /// <summary>
    /// Class responsable for the OrderReposiory and all its methods to the DB.
    /// </summary>
    public class OrderRepository
    {
        private readonly CakeFactoryContext _db;

        public OrderRepository(CakeFactoryContext context)
        {
            _db = context;
        }

        public Order GetOrderById(int id)
        {
            var order = _db.Orders
                            .Where(o => o.Id == id)
                            .FirstOrDefault();

            return order;
        }
    }
}
