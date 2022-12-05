using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class OrderHasCake
    {
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int OrderId { get; set; }
        public int CakeId { get; set; }

        public virtual Cake Cake { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
