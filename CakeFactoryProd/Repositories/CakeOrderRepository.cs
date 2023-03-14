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


        public List<AdminOrderVM> GetAllCakeOrders()
        {
            var orders = from o in _context.Orders
                         join oc in _context.OrderHasCakes on o.Id equals oc.OrderId
                         join c in _context.Cakes on oc.CakeId equals c.Id
                         join ct in _context.CakeHasToppings on c.Id equals ct.CakeId
                         join t in _context.Toppings on ct.ToppingId equals t.Id
                         join u in _context.Users on o.UserId equals u.Id

                         select new AdminOrderVM
                         {
                             Cost = oc.Cost,

                             CakeOrderVM = new CakeOrderVM
                             {
                                 OrderId = oc.OrderId,
                                 Quantity = oc.Quantity,

                                 CakeVM = new CakeVM
                                 {
                                     CakeId = c.Id,
                                     Name = c.Name,
                                     Price = c.Price,
                                     FillingId = c.FillingId,
                                     ShapeId = c.ShapeId,
                                     SizeId = c.SizeId,
                                 },

                                PickupDate = o.PickupDate,
                                PurchaseDate = o.PurchaseDate,
                                Total = o.TotalAmount,    
                             },

                             ToppingVM = new ToppingVM
                             {
                                 CakeId = c.Id,
                                 ToppingId = ct.ToppingId,
                                 Flavor = c.Filling.Flavor,
                                 PriceFactor = t.PriceFactor,
                             },

                             UserVM = new UserVM
                             {
                                 PrefferedName = u.PreferredName,
                                 PhoneNumber = u.PhoneNumber
                             }


                         };

            return orders.ToList();
        }

        public AdminOrderVM GetCakeOrderById(int id)
        {
            var orders = from o in _context.Orders
                         join oc in _context.OrderHasCakes on o.Id equals oc.OrderId
                         join c in _context.Cakes on oc.CakeId equals c.Id
                         join ct in _context.CakeHasToppings on c.Id equals ct.CakeId
                         join t in _context.Toppings on ct.ToppingId equals t.Id
                         join u in _context.Users on o.UserId equals u.Id

                         select new AdminOrderVM
                         {

                         };

            return (AdminOrderVM)orders;
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
        
        public List<Order> GetAllOrders(int id)
        {
            var orders = _context.Orders.Where(o => o.UserId == id).ToList();
            return orders;

        }
    }
}
