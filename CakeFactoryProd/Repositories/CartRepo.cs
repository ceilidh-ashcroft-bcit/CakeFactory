using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;

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
            var size = _context.Sizes.Where(s=> s.Id == sizeID).FirstOrDefault();
            var shape = _context.Shapes.Where(s=> s.Id == shapeID).FirstOrDefault();
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

        public int CreateOrder(List<CartVM> orderList, string email)
        {
            UserRepository userRepository = new UserRepository(_context);
            User user = userRepository.GetUserByEmail(email);

            Order newOrder = new Order()
            {
                PurchaseDate = DateTime.Now,
                PickupDate = orderList.FirstOrDefault().OrderVM.PickupDate,
                IsPicked = false,
                User = user,
                UserId = user.Id
            };

            _context.Orders.Add(newOrder);

            foreach (CartVM cartVM in orderList)
            {
                CakeVM cakeVM = cartVM.CakeVM;
                CakeOrderVM cakeOrderVM = cartVM.OrderVM;


                Cake newCake = new Cake()
                {
                    Name = cakeVM.Name,
                    Price = cakeVM.Price,
                    Description = cakeVM.Description,
                    IsActive = cakeVM.IsActive,
                    FillingId = cakeVM.FillingId,
                    ShapeId = cakeVM.ShapeId,
                    SizeId = cakeVM.SizeId,
                    ImagePath = cakeVM.CakeImage
                };

                OrderHasCake orderHasCake = new OrderHasCake()
                {
                    CakeId = newCake.Id,
                    OrderId = newOrder.Id,
                    Cake = newCake,
                    Order = newOrder,
                    Quantity = cakeOrderVM.Quantity,
                    Cost = cakeOrderVM.Total
                };

                newOrder.TotalAmount += orderHasCake.Cost;

                _context.Cakes.Add(newCake);
                _context.OrderHasCakes.Add(orderHasCake);
                _context.SaveChanges();
            }

            return newOrder.Id;
        }
    }
}
