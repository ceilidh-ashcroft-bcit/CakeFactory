﻿using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class UserVM
    {
        [Key]
        public string Email { get; set; }

    }
}
