using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Repositories
{
    public class FillingsRepo
    {
        private readonly CakeFactoryContext _context;
        public FillingsRepo(CakeFactoryContext context)
        {
            _context = context;
        }

        public IEnumerable<Filling> GetAllFillings()
        {
            IEnumerable<Filling> fillings =
                _context.Fillings.Select(t => new Filling()
                {
                    Id = t.Id,
                    Flavor = t.Flavor,
                    PriceFactor = t.PriceFactor,
                    Description = t.Description,
                    IsActive = t.IsActive,
                });

            return fillings;
        }
        /* For Lisa Cake Edit Page */
        public List<FillingVM> GetFillingAll()
        {
            IQueryable<FillingVM> filling = _context.Fillings.Select(f => new FillingVM { Flavor = f.Flavor, PriceFactor = f.PriceFactor, IsActive = f.IsActive });
            return filling.ToList();
        }
        public Filling GetFillingById(int id)
        {
            var topping = _context.Fillings.FirstOrDefault(t => t.Id == id);
            if (topping == null)
            {
                return new Filling();
            }

            return topping;
        }

        public string DeleteFillingById(int id)
        {
            Filling removedFilling = GetFillingById(id);
            _context.Fillings.Remove(removedFilling);

            return $"Filling with id ${id} successfully deleted";
        }

        public Tuple<Filling, string> UpdateFillingByID(FillingVM fillingVM)
        {
            Filling filling = (from t in _context.Fillings
                                where t.Id == fillingVM.Id
                                select t).FirstOrDefault();
            if (filling != null)
            {
                filling = new Filling()
                {
                    Id = filling.Id,
                    Flavor = filling.Flavor,
                    PriceFactor = filling.PriceFactor,
                    Description = filling.Description,
                    IsActive = filling.IsActive

                };

                _context.SaveChanges();
                return Tuple.Create(filling, "Updated Filling");
            }

            return Tuple.Create(new Filling(), "Failed to update topping");

        }

        public Filling CreateNewFilling(FillingVM fillingVM)
        {
            Filling newFilling = new Filling()
            {
                Id = fillingVM.Id,
                Flavor = fillingVM.Flavor,
                PriceFactor = fillingVM.PriceFactor,
                Description = fillingVM.Description,
                IsActive = fillingVM.IsActive,
            };

            _context.Fillings.Add(newFilling);

            return newFilling;
        }
    }
}
