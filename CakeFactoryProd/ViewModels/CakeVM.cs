using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CakeVM
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
        public string Size { get; set; }
        public string Shape { get; set; }


        //public int CakeId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string CakeImage { get; set; }
        //public decimal Price { get; set; }
        //public int FillingId { get; set; }
        //public int SizeId { get; set; }
        //public int ShapeId { get; set; }
        //public int ToppingId { get; set; }

    }
}
