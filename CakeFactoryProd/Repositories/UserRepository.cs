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

        public User GetUserByEmail(string email)
        {
            List<User> users = _context.Users.ToList();
            //.Where(u => u.Email == email).FirstOrDefault();
            User user = users.Where(u => u.Email == email).FirstOrDefault();

            return user;
        }


        public User GetUserProfile(string email)
        {

            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;

        }

        public string Edit(User user)
        {

            _context.Users.Update(user);
            _context.SaveChanges();
            return "";
        }
    }
}
