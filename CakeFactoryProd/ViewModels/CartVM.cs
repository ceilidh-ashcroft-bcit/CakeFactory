using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CartVM
    {
        [Key]
        public int ID { get; set; }
        public virtual CakeVM CakeVM { get; set; }
        public virtual CakeOrderVM OrderVM { get; set; }
    }
}
