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
            IQueryable<ToppingVM> topping = _context.Toppings.Select(t => new ToppingVM { ToppingId = t.Id, Flavor = t.Flavor, PriceFactor = t.PriceFactor, IsActive = t.IsActive });

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




        public List<ToppingVM> GetToppingByCakeId(int id)
        {
            List<ToppingVM> selectedToppings = (from to in _context.Toppings
                                            join ct in _context.CakeHasToppings on to.Id equals ct.ToppingId
                                            join c in _context.Cakes on ct.CakeId equals c.Id
                                            select new ToppingVM
                                            {
                                                ToppingId = to.Id,
                                                Flavor = to.Flavor,
                                                CakeId = ct.CakeId,
                                                Selected = true
                                            }).ToList();

/*            List<Topping> selectedTopping = _context.Toppings.Select(t => new ToppingVM { ToppingId = t.Id, Flavor = t.Flavor, Selected = true }).ToList();*/
            var t = selectedToppings.Where(t => t.CakeId == id);
            return t.ToList();
        }

        public string DeleteToppingById(int id)
        {
            Topping removedTopping = GetToppingById(id);
            _context.Toppings.Remove(removedTopping);
            _context.SaveChanges();

            return $"Topping with id ${id} successfully deleted";
        }
        public void DeleteCakeToppingByCakeId(int cakeId)
        {
            try
            {
                List<CakeHasTopping> toppings = _context.CakeHasToppings.Where(t => t.CakeId == cakeId).ToList();
                for (int i = 0; i < toppings.Count; i++)
                {
                    _context.CakeHasToppings.Remove(toppings[i]);
                    _context.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public Tuple<Topping, string> UpdateToppingByID(ToppingVM toppingVM)
        {
            Topping topping = (from t in _context.Toppings
                              where t.Id == toppingVM.ToppingId
                               select t).FirstOrDefault();
            if (topping != null)
            {
                topping.Id = toppingVM.ToppingId;
                topping.Flavor = toppingVM.Flavor;
                topping.PriceFactor = toppingVM.PriceFactor;
                topping.Description = toppingVM.Description;
                topping.IsActive = toppingVM.IsActive;

                _context.SaveChanges();
                return Tuple.Create(topping, "Updated Topping");
            }

            return Tuple.Create(new Topping(), "Failed to update topping");

        }

        public Topping CreateNewTopping(ToppingVM toppingVM)
        {
            Topping newTopping = new Topping()
            {
                Id = toppingVM.ToppingId,
                Flavor = toppingVM.Flavor,
                PriceFactor = toppingVM.PriceFactor,
                Description = toppingVM.Description,
                IsActive = toppingVM.IsActive,
            };

            _context.Toppings.Add(newTopping);
            _context.SaveChanges();

            return newTopping;
        }
        
        public string AddCakeHasToppings(int[] toppings, int cakeId)
        {
            foreach(int topping in toppings)
            {
                CakeHasTopping newTopping = new CakeHasTopping()
                {
                    CakeId = cakeId,
                    ToppingId = topping
                };

                _context.CakeHasToppings.Add(newTopping);
                _context.SaveChanges();
            }

            return "Successfully added";
        }
        
        public string EditCakeHasToppings(int[] toppings, int cakeId)
        {
            //delete cake topping before edit
            DeleteCakeToppingByCakeId(cakeId);

            foreach (int topping in toppings)
            {
                CakeHasTopping newTopping = new CakeHasTopping()
                {
                    CakeId = cakeId,
                    ToppingId = topping
                };

                _context.CakeHasToppings.Add(newTopping);
                _context.SaveChanges();
            }

            return "Successfully added";
        }
    }
}
