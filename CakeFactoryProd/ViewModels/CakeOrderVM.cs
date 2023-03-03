﻿using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class CakeOrderVM
    {
        public int OrderId { get; set; }
        public CakeVM? CakeVM { get; set; }

        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z-_]*$", ErrorMessage = "Please enter a valid message")]
        [MaxLength(50, ErrorMessage = "A maximum of 50 characters is only allowed")]
        public string? CustomMessage { get; set; }

        [Required(ErrorMessage = "Please select a date")]
        [BindProperty,DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PickupDate { get; set; }

        [Required(ErrorMessage = "Please enter in a quantity")]
        public int Quantity { get; set; }

        public decimal Total { get; set; }

        [Required(ErrorMessage = "Please select a size")]
        public SelectList Sizes { get; set; }

        [Required(ErrorMessage = "Please select a shape")]
        public SelectList Shapes { get; set; }

        [Required(ErrorMessage = "Please select a filling")]
        public SelectList Fillings { get; set; }

        public List<Topping>? Toppings { get; set; }

    }
}
