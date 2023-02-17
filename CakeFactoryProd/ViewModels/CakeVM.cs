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

    }
}
