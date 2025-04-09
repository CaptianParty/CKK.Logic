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
        public int quantity { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value >= 0)
                {
                    quantity = value;
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

        private ShoppingCartItem toy;
        private int v;



        public ShoppingCartItem(Product product, int quantity)
        {

            Product = product;
            Quantity = quantity;

        }

        public ShoppingCartItem(ShoppingCartItem toy, int v)
        {
            this.toy = toy;
            this.v = v;
        }


     


    }
}
