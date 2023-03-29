namespace CakeFactoryProd.ViewModels
{
    public class EmailVM
    {
        public string Email { get; set; } = null!;
        public int OrderId { get; set; }
        public string Name { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? PickupDate { get; set; }

    }
}
