using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class Topping
    {
        public int Id { get; set; }
        public string Flavor { get; set; } = null!;
        public double PriceFactor { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
