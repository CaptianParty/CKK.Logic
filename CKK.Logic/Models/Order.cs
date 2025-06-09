using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CKK.Logic.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<PurchasedItem> PurchasedItems { get; set; } = new();

    }
}
