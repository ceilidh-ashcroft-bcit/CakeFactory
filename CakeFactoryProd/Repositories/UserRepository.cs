using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;
using Microsoft.EntityFrameworkCore;

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
