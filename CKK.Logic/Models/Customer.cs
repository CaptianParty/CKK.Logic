using CKK.Logic.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Customer : Entity
    {
        //WILL NEED TO IMPLEMENT THIS ON FINAL VERSION FOR MY OWN PROJECT BY ADDING A CHECKOUT WITH USER INPUT

    
        public int ShoppingCartId { get; set; }


        public string Street { get; set; } = string.Empty;

        public string Zip { get; set; } 
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

    }
    [Serializable]
    public class PaymentInfo : Entity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        public string CardNumber { get; set; }
            
        [Required(ErrorMessage = "Expiration date required")]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVV required")]
        public string CVC { get; set; }

    }

}
