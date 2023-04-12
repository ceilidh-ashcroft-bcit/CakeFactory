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

        /// <summary>
        /// admin view for all cake orders 
        /// </summary>
        /// <returns>
        /// all cake orders
        /// </returns>
        public List<AdminOrderVM> GetAllAdminCakeOrders()
        {
            try
            {
                var orders = from o in _context.Orders
                             join oc in _context.OrderHasCakes on o.Id equals oc.OrderId
                             join c in _context.Cakes on oc.CakeId equals c.Id
                             //join ct in _context.CakeHasToppings on c.Id equals ct.CakeId
                             //join t in _context.Toppings on ct.ToppingId equals t.Id
                             join u in _context.Users on o.UserId equals u.Id

                             select new AdminOrderVM
                             {
                                 Cost = oc.Cost,
                                 IsPicked = o.IsPicked,

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

                                 //ToppingVM = new ToppingVM
                                 //{
                                 //    CakeId = c.Id,
                                 //    ToppingId = ct.ToppingId,
                                 //    Flavor = c.Filling.Flavor,
                                 //    PriceFactor = t.PriceFactor,
                                 //},

                                 UserVM = new UserVM
                                 {
                                     PrefferedName = u.PreferredName,
                                     UserName = u.Name,
                                     PhoneNumber = u.PhoneNumber
                                 }


                             };

                return orders.ToList();
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("Error retrieving admin cake orders: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Admin view for cake orders by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// cake order by id
        /// </returns>
        public AdminOrderVM GetAdminCakeOrderById(int id)
        {
            try
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

                    ToppingVM = new ToppingVM
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
            catch (Exception ex)
            {
                // Log the error message or do some other error handling.
                throw new Exception("An error occurred while retrieving the admin cake order.", ex);
            }
        }


        /// <summary>
        /// Createing the view to see all cake orders new and old
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdminOrderVM CakeOrderAdminEdit(int id)
        {
            try
            {
                Order order = _context.Orders.Find(id);

                if (order == null)
                {
                    // Throw an exception if order with the given id is not found
                    throw new Exception($"Order with id {id} not found");
                }

                return new AdminOrderVM
                {
                    IsPicked = order.IsPicked,

                    CakeOrderVM = new CakeOrderVM
                    {
                        OrderId = order.Id,
                        PickupDate = order.PickupDate,
                        PurchaseDate = order.PurchaseDate,
                    }
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving the order with id {id}: {ex.Message}");

                // Return null or a default value or re-throw the exception based on your requirements
                return null;
            }
        }

        /// <summary>
        /// Changing the order to flag if its been picked up
        /// </summary>
        /// <param name="adminOrderVM"></param>
        /// <returns></returns>
        public Tuple<Order, string> CakeOrderAdminUpdate(AdminOrderVM adminOrderVM)
        {
            try
            {
                Order order = _context.Orders.Find(adminOrderVM.CakeOrderVM.OrderId);

                if (order != null)
                {
                    order.Id = adminOrderVM.CakeOrderVM.OrderId;
                    order.IsPicked = adminOrderVM.IsPicked;

                    _context.SaveChanges();

                    return Tuple.Create(order, "Updated Order");
                }

                return Tuple.Create(new Order(), "Failed to update order");
            }
            catch (Exception ex)
            {
                return Tuple.Create(new Order(), "Error updating order: " + ex.Message);
            }
        }



        public void DeleteCakeOrder(int id)
        {
            OrderHasCake orderHasCake = _context.OrderHasCakes.Find(id);
            _context.OrderHasCakes.Remove(orderHasCake);
            _context.SaveChanges();
        }

        //public void AddCakeOrder(OrderHasCake orderHasCake)
        //{
        //    _context.OrderHasCakes.Add(orderHasCake);
        //    _context.SaveChanges();
        //}

        //public void UpdateCakeOrder(OrderHasCake orderHasCake)
        //{
        //    _context.OrderHasCakes.Update(orderHasCake);
        //    _context.SaveChanges();
        //}

        //public void DeleteCakeOrder(int id)
        //{
        //    OrderHasCake orderHasCake = _context.OrderHasCakes.Find(id);
        //    _context.OrderHasCakes.Remove(orderHasCake);
        //    _context.SaveChanges();
        //}  
        
        public List<CakeOrderVM> GetAllOrders(int id)
        {

            var query = from c in _context.Cakes
                        join oc in _context.OrderHasCakes on c.Id equals oc.CakeId
                        join o in _context.Orders on oc.OrderId equals o.Id
                        join i in _context.IPNs on o.Id equals i.OrderId into ipns
                        from ipn in ipns.DefaultIfEmpty()
                        where o.UserId == id

                        select new CakeOrderVM
                        {
                            CakeVM = new CakeVM
                            {
                                Name = c.Name
                            },

                            PurchaseDate = o.PurchaseDate,
                            Total = o.TotalAmount,
                            Quantity = oc.Quantity,
                            OrderId = oc.OrderId,

                            IpnVM = new IpnVM
                            {
                                PaymentId = (ipn == null ? null : ipn.PaymentId)
                            }
                           
                        };


            // Create a list of CakeOrderVM objects
            List<CakeOrderVM> cakeOrders = query.ToList();

            return cakeOrders;
        }
    }
}
