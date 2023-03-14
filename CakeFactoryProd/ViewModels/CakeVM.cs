using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CakeVM
    {
        public bool? IsActive { get; set; }
        public string Size { get; set; }
        public string Shape { get; set; }
        [Key]
        public int CakeId { get; set; }
        public string Name { get; set; } = "Custom Cake";
        public string Description { get; set; } = "A delicious cake made exactly to your specifications.";
        public string CakeImage { get; set; } = "~/images/emptyCake.jpg";
        public decimal Price { get; set; } = 24.99m;
        public int FillingId { get; set; }
        public string Filling { get; set; }
        public decimal FillingPrice { get; set; } = 1.99m;
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public decimal ShapePrice { get; set; } = 5.00m;
        public int ToppingId { get; set; }

        public string? ToppingList { get; set; }
        public decimal ToppingPrice { get; set; } = 1.00m;
    }
}
