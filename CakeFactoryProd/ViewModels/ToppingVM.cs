using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class ToppingVM
    {
        public int ToppingId { get; set; }
        public int CakeId { get; set; }
        public string Flavor { get; set; } = null!;
        public double PriceFactor { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
