using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CakeVM
    {
        [Key]
        public int CakeId { get; set; }
        public bool? IsActive { get; set; }
        public string Size { get; set; }
        public string Shape { get; set; }
        public string Filling { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public byte[]? ImageCake { get; set; }
        public byte[]? ImageData { get; set; }
        public IFormFile? CakeImage { get; set; }
        public decimal Price { get; set; }
        public int FillingId { get; set; }
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public int ToppingId { get; set; }

        [Required(ErrorMessage = "Please select a size")]
        public SelectList Sizes { get; set; }

        [Required(ErrorMessage = "Please select a shape")]
        public SelectList Shapes { get; set; }

        [Required(ErrorMessage = "Please select a filling")]
        public SelectList Fillings { get; set; }

        //[Required(ErrorMessage = "Please select a topping")]
        public List<Topping>? Toppings { get; set; } = new List<Topping>();

        [Required(ErrorMessage = "Please select a topping")]
        public List<ToppingVM>? SelectedToppings { get; set; } = new List<ToppingVM>();
        public int[] Accepted { get; set; }

    }
}
