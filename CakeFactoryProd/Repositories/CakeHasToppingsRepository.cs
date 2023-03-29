using CakeFactoryProd.Data;
using CakeFactoryProd.Models;

namespace CakeFactoryProd.Repositories
{
    public class CakeHasToppingsRepository
    {
        private readonly CakeFactoryContext _db;

        public CakeHasToppingsRepository(CakeFactoryContext context)
        {
            _db = context;
        }

        public Boolean CreateCHT(int cakeId, int toppingId)
        {
            try
            {
                var addingCHT = new CakeHasTopping
                {
                    CakeId = cakeId,
                    ToppingId = toppingId
                };

                _db.Add(addingCHT);
                _db.SaveChanges();

                return true;
            } catch(Exception ex)
            {
                Console.WriteLine("###Error: " + ex.ToString());
                return false;
            }
        }

        public List<CakeHasTopping> GetToppings(int cakeId)
        {
            var listOfToppings = _db.CakeHasToppings
                            .Where(cht => cht.CakeId == cakeId)
                            .ToList();

            return listOfToppings;
        }
    }
}
