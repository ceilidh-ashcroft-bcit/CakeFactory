using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models
{
    public partial class Cake
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? ImagePath { get; set; }
        public bool IsPredefined { get; set; }
        public int FillingId { get; set; }
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public virtual Filling Filling { get; set; } = null!;
        public virtual Shape Shape { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
