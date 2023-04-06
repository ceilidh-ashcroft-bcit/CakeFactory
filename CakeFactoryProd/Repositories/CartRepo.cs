using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;
using Newtonsoft.Json;

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
            var size = _context.Sizes.Where(s => s.Id == sizeID).FirstOrDefault();
            var shape = _context.Shapes.Where(s => s.Id == shapeID).FirstOrDefault();
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


        /// <summary>
        /// Method responsable for recording data in DB, tables:
        /// Order, OrderHasCakes, Cakes and IPN
        /// </summary>
        /// <param name="orderList">CartVM object</param>
        /// <param name="email">Customer's Email</param>
        /// <returns>View for displaying either Success or Error, both with information</returns>
        public int CreateOrder(List<CartVM> orderList, string email, IPN ipn)
        {
            UserRepository userRepository = new UserRepository(_context);
            User user = userRepository.GetUserByEmail(email);

            Order newOrder = new Order()
            {
                PurchaseDate = DateTime.Now,
                PickupDate = orderList.FirstOrDefault().OrderVM.PickupDate,
                IsPicked = false,
                User = user,
                UserId = user.Id,
                //TotalAmount = Decimal.Parse(ipn.Amount)
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            ipn.OrderId = newOrder.Id;
            _context.IPNs.Add(ipn);
            _context.SaveChanges();

            foreach (CartVM cartVM in orderList)
            {
                CakeVM cakeVM = cartVM.CakeVM;
                CakeOrderVM cakeOrderVM = cartVM.OrderVM;
                ToppingsRepo toppings = new ToppingsRepo(_context);

                // need to check whether Cake has Id, it means it is already in DB,
                // otherwise need to insert it in DB
                int tempCakeId = cartVM.CakeVM.CakeId;
                if (tempCakeId == 0)
                {
                    Cake newCake = new Cake()
                    {
                        Name = cakeVM.Name,
                        Price = cakeVM.Price,
                        Description = cakeVM.Description,
                        //IsActive = cakeVM.IsActive,
                        IsActive = false,
                        FillingId = cakeVM.FillingId,
                        ShapeId = cakeVM.ShapeId,
                        SizeId = cakeVM.SizeId,
                        //ImagePath = cakeVM.CakeImage
                    };
                    _context.Cakes.Add(newCake);
                    _context.SaveChanges();
                    tempCakeId = newCake.Id;
                }

                // need to loop through CakeHasToppings list to add
                // a row for each occurrancy in the table CakeHasToopin
                var toppingsAccepted = cakeVM.Accepted;
                toppings.AddCakeHasToppings(toppingsAccepted, tempCakeId);

                OrderHasCake orderHasCake = new OrderHasCake()
                    {
                        CakeId = tempCakeId,
                        OrderId = newOrder.Id,
                        //Cake = newCake,
                        Order = newOrder,
                        Quantity = cakeOrderVM.Quantity,
                        Cost = cakeOrderVM.Total
                    };

                newOrder.TotalAmount += orderHasCake.Cost;

                _context.OrderHasCakes.Add(orderHasCake);
                _context.SaveChanges();
            }

            return newOrder.Id;
        }


        public IPN GetIPNDetails(string paymentID)
        {
            try
            {
                var ipn = _context.IPNs
                        .Where(ipn => ipn.PaymentId == paymentID)
                        .FirstOrDefault()!;
                return ipn;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public IPN GetIPNDetailsByOrderId(int orderId)
        {
            try
            {
                var ipn = _context.IPNs
                        .Where(ipn => ipn.OrderId == orderId)
                        .FirstOrDefault()!;
                return ipn;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
    }
}
