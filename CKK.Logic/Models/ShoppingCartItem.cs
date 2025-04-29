using CKK.Logic.Interfaces;
using CKK.Logic.Exceptions;
namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem : InventoryItem
    {

        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int _quantity { get; set; }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value >= 0)
                {
                    _quantity = value;
                }
                else
                {
                    throw new InventoryItemStockTooLowException();
                }
            }
        }
                public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }

    }
}
