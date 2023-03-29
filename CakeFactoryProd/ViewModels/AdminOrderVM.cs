using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class AdminOrderVM
    {
        public decimal Cost { get; set; }

        public bool IsPicked { get; set; }

        public CakeOrderVM CakeOrderVM { get; set; }

        public ToppingVM ToppingVM { get; set; }

        public UserVM UserVM { get; set; }
    }
}
