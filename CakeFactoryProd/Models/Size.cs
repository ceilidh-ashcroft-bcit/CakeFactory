using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class Size
    {
        public Size()
        {
            Cakes = new HashSet<Cake>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public decimal CakeBasicPrice { get; set; }

        public virtual ICollection<Cake> Cakes { get; set; }
    }
}
