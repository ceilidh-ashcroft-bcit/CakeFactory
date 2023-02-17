using CakeFactoryProd.Models;
using CakeFactoryProd.Data;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Repositories
{
    public class CakeOrderRepository
    {
        private readonly CakeFactoryContext _context;

        public CakeOrderRepository(CakeFactoryContext context)
        {
            _context = context;
        }

        public List<OrderHasCake> GetAllCakeOrders()
        {
            return _context.OrderHasCakes.ToList();
        }

        public OrderHasCake GetCakeOrderById(int id)
        {
            return _context.OrderHasCakes.Find(id);
        }

        public void AddCakeOrder(OrderHasCake orderHasCake)
        {
            _context.OrderHasCakes.Add(orderHasCake);
            _context.SaveChanges();
        }

        public void UpdateCakeOrder(OrderHasCake orderHasCake)
        {
            _context.OrderHasCakes.Update(orderHasCake);
            _context.SaveChanges();
        }

        public void DeleteCakeOrder(int id)
        {
            OrderHasCake orderHasCake = _context.OrderHasCakes.Find(id);
            _context.OrderHasCakes.Remove(orderHasCake);
            _context.SaveChanges();
        }   
    }
}
