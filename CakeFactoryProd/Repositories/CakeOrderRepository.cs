﻿using CakeFactoryProd.Models;
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

        //public AdminOrderVM GetCakeOrderById(int id)
        //{
        //    var orders = from o in _context.Orders
        //                 join oc in _context.OrderHasCakes on o.Id equals oc.OrderId
        //                 join c in _context.Cakes on oc.CakeId equals c.Id
        //                 join ct in _context.CakeHasToppings on c.Id equals ct.CakeId
        //                 join t in _context.Toppings on ct.ToppingId equals t.Id
        //                 join u in _context.Users on o.UserId equals u.Id

        //                 select new AdminOrderVM
        //                 {

        //                 };

        //    return (AdminOrderVM)orders;
        //}

        public AdminOrderVM GetCakeOrderById(int id)
        {
            var order = (from o in _context.Orders
                         where o.Id == id
                         join oc in _context.OrderHasCakes on o.Id equals oc.OrderId
                         join c in _context.Cakes on oc.CakeId equals c.Id
                         join ct in _context.CakeHasToppings on c.Id equals ct.CakeId
                         join t in _context.Toppings on ct.ToppingId equals t.Id
                         join s in _context.Sizes on c.SizeId equals s.Id
                         join sp in _context.Shapes on c.ShapeId equals sp.Id
                         join u in _context.Users on o.UserId equals u.Id
                         select new
                         {
                             OrderId = o.Id,
                             CakeName = c.Name,
                             Cost = oc.Cost,
                             Total = o.TotalAmount,
                             OrderDate = o.PurchaseDate,
                             Filling = c.Filling.Flavor,
                             Topping = t.Flavor,
                             Size = s.Value,
                             Shape = sp.Value,
                             Quantity = oc.Quantity,
                             Name = u.Name,
                             PrefferedName = u.PreferredName,
                             PhoneNumber = u.PhoneNumber,
                             PickupDate = o.PickupDate,
                             PurchaseDate = o.PurchaseDate,      

                         }).FirstOrDefault();


            var adminOrderVM = new AdminOrderVM
            {
                Cost = order.Cost,

                CakeOrderVM = new CakeOrderVM
                {
                    OrderId = order.OrderId,
                    Quantity = order.Quantity,
                    PickupDate = order.PickupDate,
                    PurchaseDate = order.PurchaseDate,
                    Total = order.Total,

                    CakeVM = new CakeVM
                    {
                        Name = order.CakeName,
                        Filling = order.Filling,
                        Size = order.Size,
                        Shape = order.Shape,
                 
                    },
                }, 
                
                ToppingVM= new ToppingVM
                {
                    Flavor = order.Topping
                },

                UserVM = new UserVM
                {
                    UserName = order.Name,
                    PrefferedName = order.PrefferedName,
                    PhoneNumber = order.PhoneNumber
                }
            };

            return adminOrderVM;
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
