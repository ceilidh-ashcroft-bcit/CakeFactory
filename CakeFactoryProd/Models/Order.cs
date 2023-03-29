using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public bool IsPicked { get; set; }
        public decimal TotalAmount { get; set; }
        public bool OpenOrder { get; set; }
        public DateTime? OpenOrderDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<IPN> IPNs { get; set; } = null!;

    }
}
