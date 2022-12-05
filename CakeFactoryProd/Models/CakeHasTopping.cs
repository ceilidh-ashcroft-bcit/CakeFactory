using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class CakeHasTopping
    {
        public int CakeId { get; set; }
        public int ToppingId { get; set; }

        public virtual Cake Cake { get; set; } = null!;
        public virtual Topping Topping { get; set; } = null!;
    }
}
