using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CKK.Logic.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        //WILL NEED TO CHANGE THIS FOR FINAL VERSION FOR MY OWN PROJECT TO INITIATE A NEW ORDER RANDOMIZED ALL ID WITHOUT THE HARD CODE
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
