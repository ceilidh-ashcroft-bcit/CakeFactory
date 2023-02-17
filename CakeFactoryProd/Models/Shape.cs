using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.Models
{
    public partial class Shape
    {
        public Shape()
        {
            Cakes = new HashSet<Cake>();
        }

        public int Id { get; set; }

        [Display(Name = "Shape's name")]
        public string Value { get; set; } = null!;
        public double PriceFactor { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        //https://stackoverflow.com/questions/53078044/asp-for-in-checkbox-throws-and-error-in-asp-net-core

        public virtual ICollection<Cake> Cakes { get; set; }
    }
}
