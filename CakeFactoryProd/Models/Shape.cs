using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class Shape
    {
        public Shape()
        {
            Cakes = new HashSet<Cake>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public double PriceFactor { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Cake> Cakes { get; set; }
    }
}
