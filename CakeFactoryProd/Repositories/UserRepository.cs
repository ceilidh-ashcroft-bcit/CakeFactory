using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;

namespace CakeFactoryProd.Repositories
{
    public class UserRepository
    {
        CakeFactoryContext _context;

        public UserRepository(CakeFactoryContext context)
        {
            _context = context;
        }

        public string CreateRegisteredUser(User user)
        {
            string messege = "";
            _context.Add(user);
            _context.SaveChanges();
            return messege = "Successfully created a user";
        }

        public IEnumerable<UserVM> GetRegisteredUsers()
        {
            IEnumerable<UserVM> users =
            _context.Users.Select(u => new UserVM() { Email = u.Email });

            return users;

        }
    }
}
