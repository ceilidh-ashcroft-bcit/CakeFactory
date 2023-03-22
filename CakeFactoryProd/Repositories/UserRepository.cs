using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using CakeFactoryProd.ViewModels;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CakeFactoryProd.Repositories
{
    public class UserRepository
    {
        CakeFactoryContext _context;
        private IServiceProvider _serviceProvider;


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
            User user = users.Where(u => u.Email == email).FirstOrDefault()!;

            return user;
        }


        public User GetUserProfile(string email)
        {

            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;

        }

        public User GetUserProfileById(int id)
        {

            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            return user;

        }

       
        //public User GetAspUserByEmail(string email)
        //{                  
        //    var user = _context.AspNetUsers.FirstOrDefault();

        //}

        public string Edit(User user)
        {

            _context.Users.Update(user);
            _context.SaveChanges();
            return "";
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            user.IsActive = false;
            _context.Users.Update(user);
            _context.SaveChanges();
        }


        public UserRoleVM GetUserRole(int id)
        {
            var userRole = _context.UserRoles.Where(u => u.UserId == Convert.ToString(id)).FirstOrDefault();
            int userId = int.Parse(userRole.UserId);

            UserRoleVM userRoleVM = new UserRoleVM()
            {
                Role = userRole.RoleId,
               // ID =   user22Id,
            };

            return userRoleVM;
        }

        
    }
}
