using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.Models
{
    public partial class CakeHasTopping
    {
        [Key]
        public int Id { get; set; }
        public int CakeId { get; set; }
        public int ToppingId { get; set; }

        public virtual Cake Cake { get; set; } = null!;
        public virtual Topping Topping { get; set; } = null!;
    }
}
