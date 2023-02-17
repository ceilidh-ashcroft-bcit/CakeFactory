using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Repositories
{
    public class ToppingsRepo
    {
        private readonly CakeFactoryContext _context;
        public ToppingsRepo(CakeFactoryContext context)
        {
            _context = context;
        }

        public IEnumerable<Topping> GetAllToppings()
        {
            IEnumerable<Topping> toppings =
                _context.Toppings.Select(t => new Topping() { 
                Id = t.Id,
                Flavor= t.Flavor,
                PriceFactor= t.PriceFactor,
                Description= t.Description,
                IsActive= t.IsActive,
                });

            return toppings;
        }

        /* For Lisa Cake Edit Page */
        public List<ToppingVM> GetToppingAll()
        {
            IQueryable<ToppingVM> topping = _context.Toppings.Select(t => new ToppingVM { Flavor = t.Flavor, PriceFactor = t.PriceFactor, IsActive = t.IsActive });

            return topping.ToList();
        }

        public Topping GetToppingById(int id)
        {
            var topping =  _context.Toppings.FirstOrDefault(t => t.Id == id);
            if (topping == null)
            {
                return new Topping();
            }

            return topping;
        }

        public string DeleteToppingById(int id)
        {
            Topping removedTopping = GetToppingById(id);
            _context.Toppings.Remove(removedTopping);

            return $"Topping with id ${id} successfully deleted";
        }

        public Tuple<Topping, string> UpdateToppingByID(ToppingVM toppingVM)
        {
            Topping topping = (from t in _context.Toppings
                              where t.Id == toppingVM.Id
                              select t).FirstOrDefault();
            if (topping != null)
            {
                topping = new Topping()
                {
                    Id = toppingVM.Id,
                    Flavor = toppingVM.Flavor,
                    PriceFactor = toppingVM.PriceFactor,
                    Description = toppingVM.Description,
                    IsActive = toppingVM.IsActive
                };

                _context.SaveChanges();
                return Tuple.Create(topping, "Updated Topping");
            }

            return Tuple.Create(new Topping(), "Failed to update topping");

        }

        public Topping CreateNewTopping(ToppingVM toppingVM)
        {
            Topping newTopping = new Topping()
            {
                Id = toppingVM.Id,
                Flavor = toppingVM.Flavor,
                PriceFactor = toppingVM.PriceFactor,
                Description = toppingVM.Description,
                IsActive = toppingVM.IsActive,
            };

            _context.Toppings.Add(newTopping);

            return newTopping;
        }
    }
}
