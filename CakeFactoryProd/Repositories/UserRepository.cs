using CakeFactoryProd.Data;
using CakeFactoryProd.Models;

namespace CakeFactoryProd.Repositories
{
    public class UserRepository
    {
        CakeFactoryContext _db;

        public UserRepository(CakeFactoryContext context)
        {
            _db = context;
        }

        public string CreateRegisteredUser(User user)
        {
            string messege = "";
            _db.Add(user);
            _db.SaveChanges();
            return messege = "Successfully created a user";
        }
    }
}
