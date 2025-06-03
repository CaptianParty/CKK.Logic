using CKK.Logic.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Customer : Entity
    {
        public int ShoppingCartId { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; } = string.Empty;
    
    
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card Number is required")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expiration date required")]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVV required")]
        public string CVC { get; set; } = string.Empty;
    }

}
