

using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CakeEditVM
    {
        [Key]
        public int Id { get; set; }
        public virtual List<CakeVM> CakeVMs { get; set; }
        public virtual List<ToppingVM> ToppingVMs { get; set; }
        public virtual List<FillingVM> FillingVMs { get; set; }

    }
}
