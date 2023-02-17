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

        public Filling GetFillingById(int id)
        {
            var filling = _context.Fillings.FirstOrDefault(t => t.Id == id);
            if (filling == null)
            {
                return new Filling();
            }

            return filling;
        }

        public string DeleteFillingById(int id)
        {
            Filling removedFilling = GetFillingById(id);
            _context.Fillings.Remove(removedFilling);
            _context.SaveChanges();

            return $"Filling with id ${id} successfully deleted";
        }

        public Tuple<Filling, string> UpdateFillingByID(FillingVM fillingVM)
        {
            Filling filling = (from t in _context.Fillings
                                where t.Id == fillingVM.Id
                                select t).FirstOrDefault();
            if (filling != null)
            {
                filling.Id = fillingVM.Id;
                filling.Flavor = fillingVM.Flavor;
                filling.PriceFactor = fillingVM.PriceFactor;
                filling.Description = fillingVM.Description;
                filling.IsActive = fillingVM.IsActive;

                _context.SaveChanges();
                return Tuple.Create(filling, "Updated Filling");
            }

            return Tuple.Create(new Filling(), "Failed to update filling");

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
            _context.SaveChanges();

            return newFilling;
        }
    }
}
