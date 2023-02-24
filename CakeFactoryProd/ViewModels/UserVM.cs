using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.ViewModels
{
    public class UserVM
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Please enter alphabets only.")]
        public string PrefferedName { get; set; }

        [RegularExpression("^[0-9]$", ErrorMessage = "Enter numbers only.")]
        public string PhoneNumber { get; set; }

        public int TotalNumberOfOrders { get; set; }

    }
}
