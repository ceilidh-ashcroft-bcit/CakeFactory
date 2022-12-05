using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public string? PreferredName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
